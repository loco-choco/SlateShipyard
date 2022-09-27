## ControlledVanishObject
* **Type** : Public `class`
* _Inherents from `MonoBehaviour`_ 
* **Namespace** : SlateShipyard.VanishObjects

_A class to handle when a object interacts with VanishVolumes._

Because of how VanishVolumes are coded, you can only add custom interactions with the different VanishVolumes by changing the object tag or by editing the game code. So to make handling this situation easier, when an object enters the VanishVolumes trigger volume and has the ControlledVanishObject script, instead of calling the default function it will call the following specialized methods.





### Methods

---


#### Public Methods
<div class="accordion" id="methods">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnDestructionVanishDestructionVolumedestructionVolume-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnDestructionVanishDestructionVolumedestructionVolume" aria-expanded="false" aria-controls="OnDestructionVanishDestructionVolumedestructionVolume">
            OnDestructionVanish(DestructionVolume destructionVolume)
			</button>
		</h2>
		<div id="OnDestructionVanishDestructionVolumedestructionVolume" class="accordion-collapse collapse" aria-labelledby="OnDestructionVanishDestructionVolumedestructionVolume-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>OnDestructionVanish</strong><code>(DestructionVolume destructionVolume)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to Vanish in a DestructionVolume.</i></p>
				
				<p class="my-0 ms-2">Return false if you want the original code to run, and true if you don't want to</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnSupernovaDestructionVanishSupernovaDestructionVolumesupernovaDestructionVolume-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnSupernovaDestructionVanishSupernovaDestructionVolumesupernovaDestructionVolume" aria-expanded="false" aria-controls="OnSupernovaDestructionVanishSupernovaDestructionVolumesupernovaDestructionVolume">
            OnSupernovaDestructionVanish(SupernovaDestructionVolume supernovaDestructionVolume)
			</button>
		</h2>
		<div id="OnSupernovaDestructionVanishSupernovaDestructionVolumesupernovaDestructionVolume" class="accordion-collapse collapse" aria-labelledby="OnSupernovaDestructionVanishSupernovaDestructionVolumesupernovaDestructionVolume-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>OnSupernovaDestructionVanish</strong><code>(SupernovaDestructionVolume supernovaDestructionVolume)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to Vanish in a SupernovaDestructionVolume.</i></p>
				
				<p class="my-0 ms-2">Return false if you want the original code to run, and true if you don't want to</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnBlackHoleVanishBlackHoleVolumeblackHoleVolumeRelativeLocationDataentryLocation-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnBlackHoleVanishBlackHoleVolumeblackHoleVolumeRelativeLocationDataentryLocation" aria-expanded="false" aria-controls="OnBlackHoleVanishBlackHoleVolumeblackHoleVolumeRelativeLocationDataentryLocation">
            OnBlackHoleVanish(BlackHoleVolume blackHoleVolume, RelativeLocationData entryLocation)
			</button>
		</h2>
		<div id="OnBlackHoleVanishBlackHoleVolumeblackHoleVolumeRelativeLocationDataentryLocation" class="accordion-collapse collapse" aria-labelledby="OnBlackHoleVanishBlackHoleVolumeblackHoleVolumeRelativeLocationDataentryLocation-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>OnBlackHoleVanish</strong><code>(BlackHoleVolume blackHoleVolume, RelativeLocationData entryLocation)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to Vanish in a BlackHoleVolume.</i></p>
				
				<p class="my-0 ms-2">Return false if you want the original code to run, and true if you don't want to</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnWhiteHoleReceiveWarpedWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryData-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnWhiteHoleReceiveWarpedWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryData" aria-expanded="false" aria-controls="OnWhiteHoleReceiveWarpedWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryData">
            OnWhiteHoleReceiveWarped(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData)
			</button>
		</h2>
		<div id="OnWhiteHoleReceiveWarpedWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryData" class="accordion-collapse collapse" aria-labelledby="OnWhiteHoleReceiveWarpedWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryData-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>OnWhiteHoleReceiveWarped</strong><code>(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to get Warped in a WhiteHoleVolume.</i></p>
				
				<p class="my-0 ms-2">Return false if you want the original code to run, and true if you don't want to</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnWhiteHoleSpawnImmediatelyWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryDataoutboolplayerPassedThroughWarp-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnWhiteHoleSpawnImmediatelyWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryDataoutboolplayerPassedThroughWarp" aria-expanded="false" aria-controls="OnWhiteHoleSpawnImmediatelyWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryDataoutboolplayerPassedThroughWarp">
            OnWhiteHoleSpawnImmediately(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData, out bool playerPassedThroughWarp)
			</button>
		</h2>
		<div id="OnWhiteHoleSpawnImmediatelyWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryDataoutboolplayerPassedThroughWarp" class="accordion-collapse collapse" aria-labelledby="OnWhiteHoleSpawnImmediatelyWhiteHoleVolumewhiteHoleVolumeRelativeLocationDataentryDataoutboolplayerPassedThroughWarp-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>void</code> <strong>OnWhiteHoleSpawnImmediately</strong><code>(WhiteHoleVolume whiteHoleVolume, RelativeLocationData entryData, out bool playerPassedThroughWarp)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to SpawnImmediately in a WhiteHoleVolume.</i></p>
				
				<p class="my-0 ms-2">Return false if you want the original code to run, and true if you don't want to</p>
			</div>
		</div>
	</div>
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="OnTimeLoopBlackHoleVanishTimeLoopBlackHoleVolumetimeloopBlackHoleVolume-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#OnTimeLoopBlackHoleVanishTimeLoopBlackHoleVolumetimeloopBlackHoleVolume" aria-expanded="false" aria-controls="OnTimeLoopBlackHoleVanishTimeLoopBlackHoleVolumetimeloopBlackHoleVolume">
            OnTimeLoopBlackHoleVanish(TimeLoopBlackHoleVolume timeloopBlackHoleVolume)
			</button>
		</h2>
		<div id="OnTimeLoopBlackHoleVanishTimeLoopBlackHoleVolumetimeloopBlackHoleVolume" class="accordion-collapse collapse" aria-labelledby="OnTimeLoopBlackHoleVanishTimeLoopBlackHoleVolumetimeloopBlackHoleVolume-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>OnTimeLoopBlackHoleVanish</strong><code>(TimeLoopBlackHoleVolume timeloopBlackHoleVolume)</code></p>

<p class="my-0 ms-2"><i>Handles when the object is supposed to Vanish in a TimeLoopBlackHoleVolume.</i></p>
				
				<p class="my-0 ms-2">Set playerPassedThroughWarp to true if the player is passing through the blackhole attached to the object, and false if it isn't</p>
			</div>
		</div>
	</div>
</div>



### Events

---


#### Public Events
<div class="accordion" id="events">
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="DestroyComponentsOnGrow-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#DestroyComponentsOnGrow" aria-expanded="false" aria-controls="DestroyComponentsOnGrow">
            DestroyComponentsOnGrow
			</button>
		</h2>
		<div id="DestroyComponentsOnGrow" class="accordion-collapse collapse" aria-labelledby="DestroyComponentsOnGrowheading" data-bs-parent="#events">
			<div class="accordion-body">
				<p class="my-0 ms-2"><code>bool</code> <strong>DestroyComponentsOnGrow</strong>``</p>

<p class="my-0 ms-2"><i>Handles if the object components should be destroyed after passing on Vanish.</i></p>
				
				<p class="my-0 ms-2">Set it to true if you want for it to get destroyed, and false if you don't want to.</p>
			</div>
		</div>
	</div>
</div>
