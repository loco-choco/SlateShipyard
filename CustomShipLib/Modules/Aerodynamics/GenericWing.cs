//MIT License
//Copyright(c) 2020 Ivan Pensionerov

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using UnityEngine;

namespace SlateShipyard.Modules.Aerodynamics
{
    //https://www.youtube.com/watch?v=p3jDJ9FtTyM&t=225s
    //And https://ieeexplore.ieee.org/document/7152411
    //And https://github.com/gasgiant/Aircraft-Physics/blob/master/Assets/Aircraft%20Physics/Core/Scripts/AeroSurface.cs

    public class GenericWing : MonoBehaviour
    {
        public float SkinFriction;
        public float WingArea;
        public float AspectRatio;
        public float LiftSlope;
        public float ZeroLiftAngle;
        public float StallAngleHigh;
        public float StallAngleLow;

        private float FlapAngle;
        public float AirfoilChord;
        public float AirfoilSpan;

        public float FlapFraction;

        private OWRigidbody Body;

        public FluidDetector FluidDetector;

        public void Awake() 
        {
            FluidDetector = GetComponent<FluidDetector>();
        }
        public void Start() 
        {
            Body = transform.GetAttachedOWRigidbody();
        }

        public void FixedUpdate() 
        {
            Vector3 relativePos = transform.position - Body.transform.position;

            CalculateForces(FluidDetector.GetRelativeFluidVelocity(), FluidDetector.GetFluidDensity(), relativePos, out var liftForce, out Vector3 torqueForce);

            Body.AddForce(liftForce);
            Body.AddTorque(torqueForce);
        }

        public void SetFlapAngle(float angle)
        {
            FlapAngle = Mathf.Clamp(angle, -Mathf.Deg2Rad * 50, Mathf.Deg2Rad * 50);
        }

        public void CalculateForces(Vector3 worldAirVelocity, float airDensity, Vector3 relativePosition, out Vector3 liftForce, out Vector3 torqueForce)
        {
            // Accounting for aspect ratio effect on lift coefficient.
            float correctedLiftSlope = LiftSlope * AspectRatio /
               (AspectRatio + 2 * (AspectRatio + 4) / (AspectRatio + 2));

            // Calculating flap deflection influence on zero lift angle of attack
            // and angles at which stall happens.
            float theta = Mathf.Acos(2 * FlapFraction - 1);
            float flapEffectivness = 1 - (theta - Mathf.Sin(theta)) / Mathf.PI;
            float deltaLift = correctedLiftSlope * flapEffectivness * FlapEffectivnessCorrection(FlapAngle) * FlapAngle;

            float zeroLiftAoaBase = ZeroLiftAngle * Mathf.Deg2Rad;
            float zeroLiftAoA = zeroLiftAoaBase - deltaLift / correctedLiftSlope;

            float stallAngleHighBase = StallAngleHigh * Mathf.Deg2Rad;
            float stallAngleLowBase = StallAngleLow * Mathf.Deg2Rad;

            float clMaxHigh = correctedLiftSlope * (stallAngleHighBase - zeroLiftAoaBase) + deltaLift * LiftCoefficientMaxFraction(FlapFraction);
            float clMaxLow = correctedLiftSlope * (stallAngleLowBase - zeroLiftAoaBase) + deltaLift * LiftCoefficientMaxFraction(FlapFraction);

            float stallAngleHigh = zeroLiftAoA + clMaxHigh / correctedLiftSlope;
            float stallAngleLow = zeroLiftAoA + clMaxLow / correctedLiftSlope;

            // Calculating air velocity relative to the surface's coordinate system.
            // Z component of the velocity is discarded. 
            Vector3 airVelocity = transform.InverseTransformDirection(worldAirVelocity);
            airVelocity = new Vector3(0f, airVelocity.y, airVelocity.z);

            Vector3 dragDirection = transform.TransformDirection(airVelocity.normalized);
            Vector3 liftDirection = Vector3.Cross(dragDirection, transform.right);

            float area = AirfoilChord * AirfoilSpan;
            float dynamicPressure = 0.5f * airDensity * airVelocity.sqrMagnitude;
            float angleOfAttack = Mathf.Atan2(airVelocity.y, -airVelocity.z);

            Vector3 aerodynamicCoefficients = CalculateCoefficients(angleOfAttack,
                                                                    correctedLiftSlope,
                                                                    zeroLiftAoA,
                                                                    stallAngleHigh,
                                                                    stallAngleLow);

            Vector3 lift = liftDirection * aerodynamicCoefficients.x * dynamicPressure * area;
            Vector3 drag = dragDirection * aerodynamicCoefficients.y * dynamicPressure * area;
            Vector3 torque = -transform.right * aerodynamicCoefficients.z * dynamicPressure * area * AirfoilChord;


            liftForce = lift + drag;
            torqueForce = Vector3.Cross(relativePosition, liftForce);
            torqueForce += torque;
        }


        private Vector3 CalculateCoefficients(float angleOfAttack,
                                          float correctedLiftSlope,
                                          float zeroLiftAoA,
                                          float stallAngleHigh,
                                          float stallAngleLow)
        {
            Vector3 aerodynamicCoefficients;

            // Low angles of attack mode and stall mode curves are stitched together by a line segment. 
            float paddingAngleHigh = Mathf.Deg2Rad * Mathf.Lerp(15, 5, (Mathf.Rad2Deg * FlapAngle + 50) / 100);
            float paddingAngleLow = Mathf.Deg2Rad * Mathf.Lerp(15, 5, (-Mathf.Rad2Deg * FlapAngle + 50) / 100);
            float paddedStallAngleHigh = stallAngleHigh + paddingAngleHigh;
            float paddedStallAngleLow = stallAngleLow - paddingAngleLow;

            if (angleOfAttack < stallAngleHigh && angleOfAttack > stallAngleLow)
            {
                // Low angle of attack mode.
                aerodynamicCoefficients = CalculateCoefficientsAtLowAoA(angleOfAttack, correctedLiftSlope, zeroLiftAoA);
            }
            else
            {
                if (angleOfAttack > paddedStallAngleHigh || angleOfAttack < paddedStallAngleLow)
                {
                    // Stall mode.
                    aerodynamicCoefficients = CalculateCoefficientsAtStall(
                        angleOfAttack, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
                }
                else
                {
                    // Linear stitching in-between stall and low angles of attack modes.
                    Vector3 aerodynamicCoefficientsLow;
                    Vector3 aerodynamicCoefficientsStall;
                    float lerpParam;

                    if (angleOfAttack > stallAngleHigh)
                    {
                        aerodynamicCoefficientsLow = CalculateCoefficientsAtLowAoA(stallAngleHigh, correctedLiftSlope, zeroLiftAoA);
                        aerodynamicCoefficientsStall = CalculateCoefficientsAtStall(
                            paddedStallAngleHigh, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
                        lerpParam = (angleOfAttack - stallAngleHigh) / (paddedStallAngleHigh - stallAngleHigh);
                    }
                    else
                    {
                        aerodynamicCoefficientsLow = CalculateCoefficientsAtLowAoA(stallAngleLow, correctedLiftSlope, zeroLiftAoA);
                        aerodynamicCoefficientsStall = CalculateCoefficientsAtStall(
                            paddedStallAngleLow, correctedLiftSlope, zeroLiftAoA, stallAngleHigh, stallAngleLow);
                        lerpParam = (angleOfAttack - stallAngleLow) / (paddedStallAngleLow - stallAngleLow);
                    }
                    aerodynamicCoefficients = Vector3.Lerp(aerodynamicCoefficientsLow, aerodynamicCoefficientsStall, lerpParam);
                }
            }
            return aerodynamicCoefficients;
        }

        private Vector3 CalculateCoefficientsAtLowAoA(float angleOfAttack,
                                                      float correctedLiftSlope,
                                                      float zeroLiftAoA)
        {
            float liftCoefficient = correctedLiftSlope * (angleOfAttack - zeroLiftAoA);
            float inducedAngle = liftCoefficient / (Mathf.PI * AspectRatio);
            float effectiveAngle = angleOfAttack - zeroLiftAoA - inducedAngle;

            float tangentialCoefficient = SkinFriction * Mathf.Cos(effectiveAngle);

            float normalCoefficient = (liftCoefficient +
                Mathf.Sin(effectiveAngle) * tangentialCoefficient) / Mathf.Cos(effectiveAngle);
            float dragCoefficient = normalCoefficient * Mathf.Sin(effectiveAngle) + tangentialCoefficient * Mathf.Cos(effectiveAngle);
            float torqueCoefficient = -normalCoefficient * TorqCoefficientProportion(effectiveAngle);

            return new Vector3(liftCoefficient, dragCoefficient, torqueCoefficient);
        }

        private Vector3 CalculateCoefficientsAtStall(float angleOfAttack,
                                                     float correctedLiftSlope,
                                                     float zeroLiftAoA,
                                                     float stallAngleHigh,
                                                     float stallAngleLow)
        {
            float liftCoefficientLowAoA;
            if (angleOfAttack > stallAngleHigh)
            {
                liftCoefficientLowAoA = correctedLiftSlope * (stallAngleHigh - zeroLiftAoA);
            }
            else
            {
                liftCoefficientLowAoA = correctedLiftSlope * (stallAngleLow - zeroLiftAoA);
            }
            float inducedAngle = liftCoefficientLowAoA / (Mathf.PI * AspectRatio);

            float lerpParam;
            if (angleOfAttack > stallAngleHigh)
            {
                lerpParam = (Mathf.PI / 2 - Mathf.Clamp(angleOfAttack, -Mathf.PI / 2, Mathf.PI / 2))
                    / (Mathf.PI / 2 - stallAngleHigh);
            }
            else
            {
                lerpParam = (-Mathf.PI / 2 - Mathf.Clamp(angleOfAttack, -Mathf.PI / 2, Mathf.PI / 2))
                    / (-Mathf.PI / 2 - stallAngleLow);
            }
            inducedAngle = Mathf.Lerp(0, inducedAngle, lerpParam);
            float effectiveAngle = angleOfAttack - zeroLiftAoA - inducedAngle;

            float normalCoefficient = FrictionAt90Degrees(FlapAngle) * Mathf.Sin(effectiveAngle) *
                (1 / (0.56f + 0.44f * Mathf.Abs(Mathf.Sin(effectiveAngle))) -
                0.41f * (1 - Mathf.Exp(-17 / AspectRatio)));
            float tangentialCoefficient = 0.5f * SkinFriction * Mathf.Cos(effectiveAngle);

            float liftCoefficient = normalCoefficient * Mathf.Cos(effectiveAngle) - tangentialCoefficient * Mathf.Sin(effectiveAngle);
            float dragCoefficient = normalCoefficient * Mathf.Sin(effectiveAngle) + tangentialCoefficient * Mathf.Cos(effectiveAngle);
            float torqueCoefficient = -normalCoefficient * TorqCoefficientProportion(effectiveAngle);

            return new Vector3(liftCoefficient, dragCoefficient, torqueCoefficient);
        }

        private float TorqCoefficientProportion(float effectiveAngle)
        {
            return 0.25f - 0.175f * (1 - 2 * Mathf.Abs(effectiveAngle) / Mathf.PI);
        }

        private float FrictionAt90Degrees(float flapAngle)
        {
            return 1.98f - 4.26e-2f * flapAngle * flapAngle + 2.1e-1f * flapAngle;
        }

        private float FlapEffectivnessCorrection(float flapAngle)
        {
            return Mathf.Lerp(0.8f, 0.4f, (Mathf.Abs(flapAngle) * Mathf.Rad2Deg - 10) / 50);
        }

        private float LiftCoefficientMaxFraction(float flapFraction)
        {
            return Mathf.Clamp01(1 - 0.5f * (flapFraction - 0.1f) / 0.3f);
        }
    }
}
