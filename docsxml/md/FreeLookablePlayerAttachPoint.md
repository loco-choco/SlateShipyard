## FreeLookablePlayerAttachPoint
* **Type** : Public `class`
* _Inherents from `PlayerAttachPoint`_ 
* **Namespace** : SlateShipyard.PlayerAttaching

_A PlayerAttachPoint you can use to enable free look._

Because of how the free look code was made, it is only active when sitted on the ship. To be able to use it on custom ships/custom PlayerAttachPoints you can use FreeLookablePlayerAttachPoint. When the game detects that the player is attached to a FreeLookablePlayerAttachPoint it will call AllowFreeLook to know if the player should be able to use free look.



### Properties

---


#### Public Properties
<div class="accordion" id="properties">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="AllowFreeLook-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#AllowFreeLook" aria-expanded="false" aria-controls="AllowFreeLook">
            AllowFreeLook
			</button>
		</h2>
		<div id="AllowFreeLook" class="accordion-collapse collapse" aria-labelledby="AllowFreeLook-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><p class="my-0 ms-2"><code>Func&lt; bool &gt;</code></p>
</p>
<p class="my-0 ms-2"><i>The function called to know if the player attached to it should be able to free look.</i></p>
				
				
			</div>
		</div>
	</div>
</div>




