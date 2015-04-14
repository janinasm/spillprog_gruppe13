# spillprog_gruppe13

Dokumentasjon
Alpha-prototype


1. Hierarkiet i editor (main scene)
2. Prefabs
3. Layers og Tags
4. Scripts
5. TODO


1. Hierarkiet i editor (main scene)

Main Camera
Har tag “Main Camera”.


ScriptHolder
Alle script som ikke er knyttet til spesifikke prefabs er lagt til som komponent på dette gameobjectet.


Ground
Representerer spillverden. Ligger på 0,0,0 coords.


Path (prefab)
Fiendene går på nærmeste path, og går mot Landsbyen. Selve gameobjectet “Path” er ikke nødvendig, men brukes som en mal foreløpig. Paths lages ved å lage navmesh. 
Se unity docs: http://docs.unity3d.com/351/Documentation/Manual/Navmeshbaking.html

Paths må ha tag “Path” (brukes i PlaceableForsvarselement-script).


Spawnpoint (prefab)
Fiender spawner på disse. Det skal kunne legges til eller fjernes spawnpoints uten at det må gjøres endringer i scripts.

Spawnpoints har tag “Spawnpoint”.
Har et “Light”-komponent på seg, som er disabled. Brukes i Kampfase og Forberedelsesfase-script


Landsby (prefab)
	
LandsbyRange


Dynamiske Objekter
Et tomt gameobject kun ment for å holde orden i hierarkiet.

	FiendeHolder
	Fiender legges her. Brukes i kampfase-script.


GUI
ActionbarCanvas
Panelet med knapper for kjøp av forsvarselement. Brukes i fase-scripts.

	Button
	Onclick-metodene ligger i ForsvarsselementManager.
	Metodene knyttes til knappene i editoren, under Button-komponent.

Pengetext
Poengtext
Nedtellertext
Rundetext
Fasetext



2. Prefabs

Forsvarselement
Har layer og tag “Forsvarselement”
Har en Capsule-collider med trigger (brukes til å avgjøre om gameobjectet kan plasseres, er derfor litt større enn modellen.
Har en kinematic Rigidbody.
Har alle scripts knyttet til forsvarselement


SkyteRange
Har en Box-collider med trigger, brukes til å avgjøre om forsvarselementet kan skyte på fiender.
Brukes i Forsvarsangrep- og OppgraderForsvarselement-script.


ForsvarselementModel
Har en Capsule-collider med trigger som brukes av FiendeAngrep/Projektil til å skade Forsvarselement.


Skyteposisjon
Projektil instanstiater fra denne posisjonen.
Brukes i ForsvarselementAngrep.


ForsvarselementProjektil
Har Sphere-collider med trigger
Har Rigidbody


Fiende
Har tag “Fiende”.
Har kinematic Rigidbody.
Har Nav Mesh Agent, med auto repath.
Har script knyttet til Fiende


FiendeSkytePosisjon
Projektil instanstiater fra denne posisjonen.

	
FiendeModel
	Har tag “Fiende”.
	Har Capsule-collider med trigger.
.
	
FiendeSkyteRange
	Har Sphere-collider med trigger.


Projektil
Har Sphere-collider med trigger.
Har Rigidbody.


Landsby
Har tag “Landsby”.
Har Box-collider med trigger.

	
LandsbyRange
	Har Sphere-collider med trigger.


3. Layers og Tags
	


4. Scripts

På Scriptholder:

GameManager
Holder på verdier som brukes mange scripts, der det bare er nødvendig med en instance av det.


ForsvarselementManager
Har metodene som blir brukt på onclick på buttons i actionbar. Sier hvilket gameobject som skal instantiate og trekker fra penger.


ForsvarselementPlacement
Instansiater gameobject valgt i ForsvarselementManager.

Håndterer plassering av gameobject. Finner mus-posisjonen i Vector3-verdier.

Sjekker om listen fra PlaceableForsvarselement er tom (om det ikke er gameobjects med spesifikke tags som kolliderer med objectet.

Håndterer velging av plasserte gameobjekts. Bruker raycast til å sjekke om det spilleren trykker på er et forsvarselement.


FaseGUI
Inneholder referanser til canvas-elementer.


FaseSkifte
Håndterer skifte mellom Forberedelsesfase og Kampfase. Teller ned i forberedelsesfase.


Forberedelsesfase
Lager liste over spawnpoints. Lager en liste over tilfeldige spawnpoints fiendene skal spawne ved i kampfasen. Listen brukes også til å enable Lights på spawnpoints.


Kampfase
Håndterer spawning av fiender. Sjekker om spilleren har drept alle fiender. Disabler og resetter Lights på spawnpoints og sletter listen over tilfeldige spawnpoints.
	

Poeng
Har metode for å legge til penger.


Penger
Har metoder for å legge til og trekke fra penger.


På forsvarselement:

Forsvarselement
Inneholder base verdiene (stats som helse, level osv) til forsvarselement..


ForsvarselementAngrep
Har metoder for å angripe fiender. Sjekker om det er fiender som kolliderer med “SkyteRange”. Instanstiater projektil-gameobject som har et eget script.


ForsvarselementProjektil
Sjekker om projektilen treffer noe, om det treffer fiende kjøres metode i fiendeHelse som håndterer helse.


ForsvarselementHelse
Har metode for å ta imot skade, og sjekke om gameobject fortsatt lever.


ForsvarselementRotasjon
Roterer forsvarselementet


SlettForsvarselement
Har metode for å slette forsvarselement, og håndterer penger spilleren skal få basert på gameobject level.


OppgraderForsvarselement
Har metode for å oppgradere forsvarselement. Håndterer økning av level, kostnad, og hva som skal økes av stats osv. Foreløpig endres helse, skade, og størrelse på Box-collider (range).


PlaceableForsvarselement
Sjekker om gameobjekt kolliderer med diverse tags. Legger til og fjerner fra en liste.


SelectedForsvarselement
Lager en meny når gameobjekt er valgt av spiller. Kjører metodene for flytt, oppgrader og slett på onlick


På fiende:

Fiende
FiendeAngrep
FiendeHelse
FiendeProjektil

FiendePathfinding
Her settes målet til fiendene.


På landsby:

Landsby
LandsbyAngrep
LandsbyHelse





5. TODO


Må gjøres:

Forsvarselement kan plasseres utenfor spillverden, trenger en sjekk om det er treffer/ikke treffer tag.
Visualisere HP (helse). Enten HP-bar eller tall (f.eks. 34/40). Kan gjøres ved å lage et Screen Space canvas med ui-elementene som legges på relevante gameobjects.
Landsby har per nå ingen projektil, den gjør skade. Må bestemme hvordan den skal angripe.
Lage startscreen-scene. Lage script som håndterer nytt start av spill.
Lage gameover.
Lage persistent lagring av filer og highscore.

Bør gjøres:

Menyen som lages i SelectedForsvarselement er laget med det gamle GUI-systemet. Bør (eller må) gjøres om til 4.6 UI for at det skal bli lettere å sette opp grafikk.


Ekstra:

ForsvarselementPlacement-script kan deles opp i 2 scripts: Et for plassering og et som håndterer velging av gameobjects som er plassert.
Fase-scripts er litt rotete. Noe kan flyttes til GameManager, eller til Forberedelesesfase. Kan være egne script for lys og spawning av fiender.
Rydde i scripts, optimalisere kode, kommentere bedre.


Bugs:

Notater:
