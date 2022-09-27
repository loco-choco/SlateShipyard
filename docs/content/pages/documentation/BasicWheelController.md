## BasicWheelController
* **Type** : Public `class`
* _Inherents from `MonoBehaviour`_ 
* **Namespace** : SlateShipyard.Modules.Wheels

_A basic controller to control vehicles with wheels._

This basic controller is to be used by vehicles with the two front wheels being the ones generating the force. It was made to be able to exapanded on with the virtual methods.



### Properties

---


#### Public Properties
<div class="accordion" id="properties">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="frontRWheel-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#frontRWheel" aria-expanded="false" aria-controls="frontRWheel">
            frontRWheel
			</button>
		</h2>
		<div id="frontRWheel" class="accordion-collapse collapse" aria-labelledby="frontRWheel-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>OWSimpleRaycastWheel</code></p>
</p>
<p class="my-0 ms-2"><i>The front right wheel.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="frontLWheel-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#frontLWheel" aria-expanded="false" aria-controls="frontLWheel">
            frontLWheel
			</button>
		</h2>
		<div id="frontLWheel" class="accordion-collapse collapse" aria-labelledby="frontLWheel-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>OWSimpleRaycastWheel</code></p>
</p>
<p class="my-0 ms-2"><i>The front left wheel.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="body-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#body" aria-expanded="false" aria-controls="body">
            body
			</button>
		</h2>
		<div id="body" class="accordion-collapse collapse" aria-labelledby="body-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>OWRigidbody</code></p>
</p>
<p class="my-0 ms-2"><i>The vehicle body.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="maxSteerAngle-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#maxSteerAngle" aria-expanded="false" aria-controls="maxSteerAngle">
            maxSteerAngle
			</button>
		</h2>
		<div id="maxSteerAngle" class="accordion-collapse collapse" aria-labelledby="maxSteerAngle-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The max angle the wheels can go.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="maxAccelerationForce-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#maxAccelerationForce" aria-expanded="false" aria-controls="maxAccelerationForce">
            maxAccelerationForce
			</button>
		</h2>
		<div id="maxAccelerationForce" class="accordion-collapse collapse" aria-labelledby="maxAccelerationForce-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The max force the "motor" can reach.</i></p>
				
				
			</div>
		</div>
	</div>
</div>



### Methods

---


#### Public Methods
<div class="accordion" id="methods">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="FixedUpdate-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#FixedUpdate" aria-expanded="false" aria-controls="FixedUpdate">
            FixedUpdate()
			</button>
		</h2>
		<div id="FixedUpdate" class="accordion-collapse collapse" aria-labelledby="FixedUpdate-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>void</code> <strong>FixedUpdate</strong><code>()</code></p>

<p class="my-0 ms-2"><i>When the motor force it got from the inputs is added.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="Update-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Update" aria-expanded="false" aria-controls="Update">
            Update()
			</button>
		</h2>
		<div id="Update" class="accordion-collapse collapse" aria-labelledby="Update-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>void</code> <strong>Update</strong><code>()</code></p>

<p class="my-0 ms-2"><i>When the wheels steer angle it got from the inputs is changed.</i></p>
				
				
			</div>
		</div>
	</div>
</div>


