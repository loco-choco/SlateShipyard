## VanishVolumesPatches
* **Type** : Public `class`

* **Namespace** : SlateShipyard.VanishObjects

_Prefixes patches to make the functionality of ControlledVanishObject possible._







### Methods

---


#### Public Methods
<div class="accordion" id="methods">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="ConditionsForPlayerToWarp-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#ConditionsForPlayerToWarp" aria-expanded="false" aria-controls="ConditionsForPlayerToWarp">
            ConditionsForPlayerToWarp()
			</button>
		</h2>
		<div id="ConditionsForPlayerToWarp" class="accordion-collapse collapse" aria-labelledby="ConditionsForPlayerToWarp-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>delegate bool</code> <strong>ConditionsForPlayerToWarp</strong><code>()</code></p>

				<p class="my-0 ms-2">delegate for the OnConditionsForPlayerToWarp event.</p>
			</div>
		</div>
	</div>
</div>



### Events

---


#### Public Static Events
<div class="accordion" id="eventsStatic">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnConditionsForPlayerToWarp-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnConditionsForPlayerToWarp" aria-expanded="false" aria-controls="OnConditionsForPlayerToWarp">
            OnConditionsForPlayerToWarp
			</button>
		</h2>
		<div id="OnConditionsForPlayerToWarp" class="accordion-collapse collapse" aria-labelledby="OnConditionsForPlayerToWarp-heading" data-bs-parent="#eventsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>ConditionsForPlayerToWarp</code> <strong>OnConditionsForPlayerToWarp</strong>``</p>

<p class="my-0 ms-2"><i>Event for when the player warps.</i></p>
				
				<p class="my-0 ms-2">Add to this event if you want to controll when the player can warp. For example, if the player is attached to your ship you probably don't want for the player to warp separately.</p>
			</div>
		</div>
	</div>
</div>
