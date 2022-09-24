## {{name}}
* **Type** : {{protection | string.downcase | string.capitalize}} {{kind | string.downcase}}
{{if base_ref != empty}}* _Inherents from **{{base_ref}}**_ {{end}}
* **Namespace** : {{namespace}}

{{~if briefdescription != ""~}}_{{briefdescription | string.rstrip}}_

{{~end~}}
{{if detaileddescription != ""}}{{detaileddescription | string.rstrip}}{{end}}
{{properties = get_members kind: "variable" prot: "public" is_static: "no"}}
{{if properties.size > 0}}
### Public Properties
<div class="accordion" id="properties">
{{~ for $member in properties ~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$member.name}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$member.name}}" aria-expanded="false" aria-controls="{{$member.name}}">
            {{$member.name}}
			</button>
		</h2>
		<div id="{{$member.name}}" class="accordion-collapse collapse" aria-labelledby="{{$member.name}}-heading" data-bs-parent="#properties">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>{{$member.prot | string.downcase | string.capitalize}}</b> {{$member.type}}</p>
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{staticProperties = get_members kind: "variable" prot: "public" is_static: "yes"}}
{{if staticProperties.size > 0}}
### Public Static Properties
<div class="accordion" id="propertiesStatic">
{{~ for $member in staticProperties ~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$member.name}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$member.name}}" aria-expanded="false" aria-controls="{{$member.name}}">
            {{$member.name}}
			</button>
		</h2>
		<div id="{{$member.name}}" class="accordion-collapse collapse" aria-labelledby="{{$member.name}}-heading" data-bs-parent="#propertiesStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static {{$member.prot | string.downcase | string.capitalize}}</b> {{$member.type}}</p>
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{methods = get_members kind: "function" prot: "public" is_static: "no"}}
{{if methods.size > 0}}
### Public Methods
<div class="accordion" id="methods">
{{~ for $member in methods~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$member.name}}{{$member.argsstring}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$member.name}}{{$member.argsstring}}" aria-expanded="false" aria-controls="{{$member.name}}{{$member.argsstring}}">
            {{$member.name}}{{$member.argsstring}}
			</button>
		</h2>
		<div id="{{$member.name}}{{$member.argsstring}}" class="accordion-collapse collapse" aria-labelledby="{{$member.name}}{{$member.argsstring}}-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>{{$member.prot | string.downcase | string.capitalize}}</b> {{$member.type}} {{$member.name}}{{$member.argsstring}}</p
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{methodsStatic = get_members kind: "function" prot: "public" is_static: "yes"}}
{{if methodsStatic.size > 0}}
### Public Static Methods
<div class="accordion" id="methodsStatic">
{{~ for $member in methodsStatic~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$member.name}}{{$member.argsstring}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$member.name}}{{$member.argsstring}}" aria-expanded="false" aria-controls="{{$member.name}}{{$member.argsstring}}">
            {{$member.name}}{{$member.argsstring}}
			</button>
		</h2>
		<div id="{{$member.name}}" class="accordion-collapse collapse" aria-labelledby="{{$member.name}}-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				<p class="my-0 ms-2"><b>Static {{$member.prot | string.downcase | string.capitalize}}</b> {{$member.type}} {{$member.name}}{{$member.argsstring}}</p>
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}