## ShipSpawnerManager
* **Type** : Public class

* **Namespace** : SlateShipyard.ShipSpawner

_Where all the ships from addons is stored._

This is the class you want to call if you want to access a ship or the ships added by addons.





### Methods

---


#### Public Static Methods
<div class="accordion" id="methodsStatic">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="AddShipFuncGameObjectprefabstringname-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#AddShipFuncGameObjectprefabstringname" aria-expanded="false" aria-controls="AddShipFuncGameObjectprefabstringname">
            AddShip(Func< GameObject > prefab, string name)
			</button>
		</h2>
		<div id="AddShipFuncGameObjectprefabstringname" class="accordion-collapse collapse" aria-labelledby="AddShipFuncGameObjectprefabstringname-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static Public</b> bool AddShip(Func< GameObject > prefab, string name)</p>
<p class="my-0 ms-2"><i>Adds the addon ship to the table of accessable addon ships.</i></p>
				
				<p class="my-0 ms-2">Returns false if there is already a ship with the same name, and true if it succeeded</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="RemoveShipstringname-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#RemoveShipstringname" aria-expanded="false" aria-controls="RemoveShipstringname">
            RemoveShip(string name)
			</button>
		</h2>
		<div id="RemoveShipstringname" class="accordion-collapse collapse" aria-labelledby="RemoveShipstringname-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static Public</b> bool RemoveShip(string name)</p>
<p class="my-0 ms-2"><i>Removes the addon ship from the table of accessable addon ships.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="ShipAmount-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#ShipAmount" aria-expanded="false" aria-controls="ShipAmount">
            ShipAmount()
			</button>
		</h2>
		<div id="ShipAmount" class="accordion-collapse collapse" aria-labelledby="ShipAmount-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static Public</b> int ShipAmount()</p>
<p class="my-0 ms-2"><i>The amount of ships in the table of accessable addon ships.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="GetShipDataintindex-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#GetShipDataintindex" aria-expanded="false" aria-controls="GetShipDataintindex">
            GetShipData(int index)
			</button>
		</h2>
		<div id="GetShipDataintindex" class="accordion-collapse collapse" aria-labelledby="GetShipDataintindex-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static Public</b> ShipData GetShipData(int index)</p>
<p class="my-0 ms-2"><i>Returns the ship in the table of accessable addon ships by passing its index.</i></p>
				
				
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="GetShipDatastringname-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#GetShipDatastringname" aria-expanded="false" aria-controls="GetShipDatastringname">
            GetShipData(string name)
			</button>
		</h2>
		<div id="GetShipDatastringname" class="accordion-collapse collapse" aria-labelledby="GetShipDatastringname-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static Public</b> ShipData GetShipData(string name)</p>
<p class="my-0 ms-2"><i>Returns the ship in the table of accessable addon ships by passing its name.</i></p>
				
				
			</div>
		</div>
	</div>
</div>


