## OWSimpleRaycastWheel
* **Type** : Public `class`
* _Inherents from `MonoBehaviour`_ 
* **Namespace** : SlateShipyard.Modules.Wheels

_This is a script which allows you to have simple wheel simulations with suspension, friction and other stuff._

This simulation uses a single raycast to represent the wheel and its suspension, if you want to have a more in depth look on how it works [watch these series of videos](https://www.youtube.com/watch?v=x0LUiE0dxP0).



### Properties

---


#### Public Properties
<div class="accordion" id="properties">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="wheelRadius-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#wheelRadius" aria-expanded="false" aria-controls="wheelRadius">
            wheelRadius
			</button>
		</h2>
		<div id="wheelRadius" class="accordion-collapse collapse" aria-labelledby="wheelRadius-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The radius of the wheel.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="restLenght-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#restLenght" aria-expanded="false" aria-controls="restLenght">
            restLenght
			</button>
		</h2>
		<div id="restLenght" class="accordion-collapse collapse" aria-labelledby="restLenght-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The lenght which the suspension considers to be at rest.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="springTravel-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#springTravel" aria-expanded="false" aria-controls="springTravel">
            springTravel
			</button>
		</h2>
		<div id="springTravel" class="accordion-collapse collapse" aria-labelledby="springTravel-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The distance which the suspension can travel.</i></p>
				
				<p class="my-0 ms-2">The max and min lenght it can have are calculated from this equation: minLenght = restLenght - springTravel; maxLenght = restLenght + springTravel.</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="springStiffness-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#springStiffness" aria-expanded="false" aria-controls="springStiffness">
            springStiffness
			</button>
		</h2>
		<div id="springStiffness" class="accordion-collapse collapse" aria-labelledby="springStiffness-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The spring constant of the suspension.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="damperStiffness-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#damperStiffness" aria-expanded="false" aria-controls="damperStiffness">
            damperStiffness
			</button>
		</h2>
		<div id="damperStiffness" class="accordion-collapse collapse" aria-labelledby="damperStiffness-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The damping constant of the suspension.</i></p>
				
				<p class="my-0 ms-2">Usually make it smaller then the springStiffness to have better results.</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="frictionCoeficient-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#frictionCoeficient" aria-expanded="false" aria-controls="frictionCoeficient">
            frictionCoeficient
			</button>
		</h2>
		<div id="frictionCoeficient" class="accordion-collapse collapse" aria-labelledby="frictionCoeficient-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The coeficient for the friction the wheel will experience for its foward direction velocity when touching a ground.</i></p>
				
				<p class="my-0 ms-2">Usually make it a value between 1.0 and 0.0, 0.0 being like driving in ice and 1.0 like in glue.</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="steeringFrictionCoeficient-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#steeringFrictionCoeficient" aria-expanded="false" aria-controls="steeringFrictionCoeficient">
            steeringFrictionCoeficient
			</button>
		</h2>
		<div id="steeringFrictionCoeficient" class="accordion-collapse collapse" aria-labelledby="steeringFrictionCoeficient-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The coeficient for the friction the wheel will experience for velocities that aren't on the wheel direction.</i></p>
				
				<p class="my-0 ms-2">This means that if the wheel is sliding, this friction force will try to reduce this motion.</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="steerAngle-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#steerAngle" aria-expanded="false" aria-controls="steerAngle">
            steerAngle
			</button>
		</h2>
		<div id="steerAngle" class="accordion-collapse collapse" aria-labelledby="steerAngle-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The target angle (in degrees) which you want the wheel to be at.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="steerTime-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#steerTime" aria-expanded="false" aria-controls="steerTime">
            steerTime
			</button>
		</h2>
		<div id="steerTime" class="accordion-collapse collapse" aria-labelledby="steerTime-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>float</code></p>
</p>
<p class="my-0 ms-2"><i>The 'time' that the wheel will take to reach the target angle in steerAngle.</i></p>
				
				<p class="my-0 ms-2">Bigger values make it reach the end value faster.</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="rb-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#rb" aria-expanded="false" aria-controls="rb">
            rb
			</button>
		</h2>
		<div id="rb" class="accordion-collapse collapse" aria-labelledby="rb-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>Rigidbody</code></p>
</p>
<p class="my-0 ms-2"><i>The Rigidbody the wheel will use to apply the forces it calculates.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="collisionMask-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collisionMask" aria-expanded="false" aria-controls="collisionMask">
            collisionMask
			</button>
		</h2>
		<div id="collisionMask" class="accordion-collapse collapse" aria-labelledby="collisionMask-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>LayerMask</code></p>
</p>
<p class="my-0 ms-2"><i>The LayerMask it will use on the raycast to be considered as valid ground.</i></p>
				
				
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
           <button id="IsOnGround-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#IsOnGround" aria-expanded="false" aria-controls="IsOnGround">
            IsOnGround()
			</button>
		</h2>
		<div id="IsOnGround" class="accordion-collapse collapse" aria-labelledby="IsOnGround-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>IsOnGround</strong><code>()</code></p>

<p class="my-0 ms-2"><i>Returns true if the wheel is hitting a valid ground. False if not.</i></p>
				
				
			</div>
		</div>
	</div>
</div>


