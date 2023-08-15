## {{name}}
* **Type** : {{protection | string.downcase | string.capitalize}} `{{kind | string.downcase}}`
{{if base_ref != empty}}* _Inherents from `{{base_ref}}`_ {{end}}
* **Namespace** : {{namespace}}

{{~if briefdescription != ""~}}_{{briefdescription | string.rstrip}}_

{{~end~}}
{{if detaileddescription != ""}}{{detaileddescription | string.rstrip}}{{end}}
{{properties = get_members kind: "variable" prot: "public" is_static: "no"}}
{{staticProperties = get_members kind: "variable" prot: "public" is_static: "yes"}}
{{if properties.size > 0 || staticProperties.size > 0}}
### Properties

---

{{if properties.size > 0}}
#### Public Properties
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
				<p class="my-0 ms-2">{{ $member.type | string.append "`" | string.prepend "`" | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}</p>
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{if staticProperties.size > 0}}
#### Public Static Properties
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
				{{$member.type | string.append "`" | string.prepend "`" | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{~ end ~}}
{{methods = get_members kind: "function" prot: "public" is_static: "no"}}
{{methodsStatic = get_members kind: "function" prot: "public" is_static: "yes"}}
{{if methods.size > 0 || methodsStatic.size > 0}}
### Methods

---

{{if methods.size > 0}}
#### Public Methods
<div class="accordion" id="methods">
{{~ for $member in methods~}}
{{~ $memberId = $member.name+$member.argsstring| regex.replace "([<>() ,.])" "" | string.rstrip~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$memberId}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$memberId}}" aria-expanded="false" aria-controls="{{$memberId}}">
            {{$member.name}}{{$member.argsstring}}
			</button>
		</h2>
		<div id="{{$memberId}}" class="accordion-collapse collapse" aria-labelledby="{{$memberId}}-heading" data-bs-parent="#methods">
			<div class="accordion-body">
				{{"`" + $member.type + "` **" + $member.name+ "**`" + $member.argsstring+ "`"  | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{if methodsStatic.size > 0}}
#### Public Static Methods
<div class="accordion" id="methodsStatic">
{{~ for $member in methodsStatic~}}
{{~ $memberId = "static" + $member.name + $member.argsstring| regex.replace "([<>() ,.])" "" | string.rstrip~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$memberId}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$memberId}}" aria-expanded="false" aria-controls="{{$memberId}}">
            {{$member.name}}{{$member.argsstring}}
			</button>
		</h2>
		<div id="{{$memberId}}" class="accordion-collapse collapse" aria-labelledby="{{$memberId}}-heading" data-bs-parent="#methodsStatic">
			<div class="accordion-body">
				{{"`" + $member.type + "` **" + $member.name+ "**`" + $member.argsstring+ "`"  | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{~ end ~}}
{{events = get_members kind: "event" prot: "public" is_static: "no"}}
{{eventsStatic = get_members kind: "event" prot: "public" is_static: "yes"}}
{{if events.size > 0 || eventsStatic.size > 0}}
### Events

---

{{if events.size > 0}}
#### Public Events
<div class="accordion" id="events">
{{~ for $member in events~}}
{{~ $memberId = $member.name+$member.argsstring| regex.replace "([<>() ,.])" "" | string.rstrip~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$memberId}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$memberId}}" aria-expanded="false" aria-controls="{{$member.name}}">
            {{$member.name}}
			</button>
		</h2>
		<div id="{{$memberId}}" class="accordion-collapse collapse" aria-labelledby="{{$memberId}}heading" data-bs-parent="#events">
			<div class="accordion-body">
				{{"`" + $member.type + "` **"+$member.name+ "**`" + $member.argsstring + "`"  | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{if eventsStatic.size > 0}}
#### Public Static Events
<div class="accordion" id="eventsStatic">
{{~ for $member in eventsStatic~}}
{{~ $memberId = $member.name+$member.argsstring| regex.replace "([<>() ,.])" "" | string.rstrip~}}
	<div class="accordion-item">
		<h2 class="accordion-header">
           <button id="{{$memberId}}-heading" class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#{{$memberId}}" aria-expanded="false" aria-controls="{{$memberId}}">
            {{$member.name}}{{$member.argsstring}}
			</button>
		</h2>
		<div id="{{$memberId}}" class="accordion-collapse collapse" aria-labelledby="{{$memberId}}-heading" data-bs-parent="#eventsStatic">
			<div class="accordion-body">
				{{"`" + $member.type + "` **" +$member.name+ "**`" + $member.argsstring+ "`"  | to_html | string.replace '<p>' '<p class="my-0 ms-2">'}}
				{{~if $member.briefdescription != ""~}}<p class="my-0 ms-2"><i>{{$member.briefdescription | string.rstrip}}</i></p>
				
				{{~end~}}
				{{if $member.detaileddescription != ""}}<p class="my-0 ms-2">{{$member.detaileddescription | string.rstrip}}</p>{{end}}
			</div>
		</div>
	</div>
{{~ end ~}}
</div>
{{~ end ~}}
{{~ end ~}}