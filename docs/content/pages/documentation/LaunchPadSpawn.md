## LaunchPadSpawn
* **Type** : Public `class`
* _Inherents from `MonoBehaviour`_ 
* **Namespace** : SlateShipyard.ShipSpawner

_Spawner of the ship addons added on ShipSpawnerManager._

Giving the prefab function, this will spawn on the best place a ship (WIP).





### Methods

---


#### Public Methods
<div class="accordion" id="methods">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="Start-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#Start" aria-expanded="false" aria-controls="Start">
            Start()
			</button>
		</h2>
		<div id="Start" class="accordion-collapse collapse" aria-labelledby="Start-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>void</code> <strong>Start</strong><code>()</code></p>

<p class="my-0 ms-2"><i>The Start function of a MonoBehaviour.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="SpawnShipFuncGameObjectshipPrefabboolspawnEvenIfNotAllowed-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#SpawnShipFuncGameObjectshipPrefabboolspawnEvenIfNotAllowed" aria-expanded="false" aria-controls="SpawnShipFuncGameObjectshipPrefabboolspawnEvenIfNotAllowed">
            SpawnShip(Func< GameObject > shipPrefab, bool spawnEvenIfNotAllowed)
			</button>
		</h2>
		<div id="SpawnShipFuncGameObjectshipPrefabboolspawnEvenIfNotAllowed" class="accordion-collapse collapse" aria-labelledby="SpawnShipFuncGameObjectshipPrefabboolspawnEvenIfNotAllowed-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>SpawnShip</strong><code>(Func&lt; GameObject &gt; shipPrefab, bool spawnEvenIfNotAllowed)</code></p>

<p class="my-0 ms-2"><i>Spawns a ship giving the shipPrefab function.</i></p>
				
				<p class="my-0 ms-2">If spawnEvenIfNotAllowed is set to false, it will first check to see if the ship will spawn inside something and will not spawn it if that is the case (WIP). If it is set to true it will ignore that check. The check feature is still WIP so set spawnEvenIfNotAllowed to true if you want to be able to use the function.</p>
			</div>
		</div>
	</div>
</div>

#### Public Static Methods
<div class="accordion" id="methodsStatic">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="GetCombinedBoundingBoxOfChildrenTransformroot-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#GetCombinedBoundingBoxOfChildrenTransformroot" aria-expanded="false" aria-controls="GetCombinedBoundingBoxOfChildrenTransformroot">
            GetCombinedBoundingBoxOfChildren(Transform root)
			</button>
		</h2>
		<div id="GetCombinedBoundingBoxOfChildrenTransformroot" class="accordion-collapse collapse" aria-labelledby="GetCombinedBoundingBoxOfChildrenTransformroot-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>Bounds</code> <strong>GetCombinedBoundingBoxOfChildren</strong><code>(Transform root)</code></p>

<p class="my-0 ms-2"><i>Gets the Bounds of a object combined collider.</i></p>
				
				
			</div>
		</div>
	</div>
</div>


