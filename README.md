# Ghidoarca_Petru-Cristi_Grupa3134A
 
# Tema Laborator 2  
bash
# Raspunsuri intrebari



## 1. Viewport
Un viewport în OpenGL reprezintă o regiune sau porțiune a ferestrei grafice în care se va efectua procesul de randare a conținutului. De obicei, acesta este definit prin coordonatele sale în cadrul ferestrei și servește ca zona în care se desenează diverse obiecte și elemente grafice.

## 2. Frames Per Second (FPS)
FPS (Frames Per Second) reprezintă numărul de cadre sau imagini generate pe secundă în bibliotecile OpenGL. Cu cât rata FPS-ului este mai mare, cu atât conținutul este afișat mai fluid și mai rapid.

## 3. Metoda OnUpdateFrame()
Metoda `OnUpdateFrame()` este apelată în bucla principală a OpenGL și este folosită pentru a actualiza starea jocului sau a scenei respective în fiecare cadru.

## 4. Modul imediat de randare
Modul imediat (immediate mode) de randare în OpenGL se referă la o tehnică mai veche de desenare în care obiectele sunt create direct folosind funcțiile `glBegin()` și `glEnd()`. Această metodă nu mai este susținută și nu este recomandată în versiunile mai noi ale OpenGL.

## 5. Ultima versiune care acceptă modul imediat
OpenGL 2.1 este ultima versiune care oferă suport pentru modul imediat. Începând cu versiunea 3.0 și în continuare, acest mod de desenare nu mai este furnizat sau susținut.

## 6. Metoda OnRenderFrame()
Metoda `OnRenderFrame()` este apelată în bucla principală a aplicației OpenGL și este utilizată pentru a desena sau randează scena și obiectele pe ecran în fiecare cadru, pas cu pas.

## 7. Metoda OnResize()
Metoda `OnResize()` este apelată cel puțin o dată pentru a determina dimensiunile ferestrei de randare. Această metodă actualizează matricea de proiecție și se asigură că randarea este efectuată corect atunci când fereastra este redimensionată de către utilizator.

## 8. CreatePerspectiveFieldOfView()
Funcția `CreatePerspectiveFieldOfView()` din OpenGL construiește o matrice de proiecție perspectivă. Această funcție are trei parametri importanți: unghiul de vedere vertical (FOV - câmpul vizual), raportul de aspect al ferestrei și distanța dintre planurile de proiecție apropiat și îndepărtat. Acești parametri controlează modul în care este reprezentată perspectiva camerei.

- FOV (Field of View): Acesta este un unghi exprimat în grade și reprezintă câmpul vizual al camerei, adică cât de mult poți vedea într-o anumită direcție. Cu cât FOV este mai mare, cu atât câmpul vizual este mai larg.

- Raportul de aspect (Aspect Ratio): Acesta reprezintă raportul dintre lățimea și înălțimea ferestrei de afișare. Acest parametru este important pentru a asigura că obiectele sunt reprezentate corect, fără distorsiuni.

- Planul apropiat și planul îndepărtat: Acești doi parametri specifică distanța dintre camera și planurile de proiecție apropiat și îndepărtat. Planul apropiat este cel mai apropiat punct din fața camerei care va fi vizibil, iar planul îndepărtat este cel mai îndepărtat punct care va fi vizibil. Acești parametri controlează intervalul de adâncime al scenei vizibile.

### Exemplu de utilizare a funcției:
```c#
Matrix4 perspective = CreatePerspectiveFieldOfView(45.0f, aspectRatio, 0.1f, 100.0f);
```

În acest exemplu, funcția `CreatePerspectiveFieldOfView()` creează o matrice de proiecție perspectivă cu un unghi de vedere vertical de 45 de grade, un raport de aspect dat de variabila `aspectRatio`, și planurile de proiecție apropiat și îndepărtat la distanțele 0.1f și 100.0f, respectiv. Această matrice de proiecție poate fi utilizată pentru a transforma coordonatele obiectelor în coordonate de ecran, luând în considerare perspectiva camerei.
# Tema Laborator 3  
bash
# Raspunsuri intrebari
## 1. Ordinea de desenare a vertexurilor

  GL.Begin(PrimitiveType.Lines);

            // X(culoare rosie)
            GL.Color3(Color.Red);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(100, 10, 10);

            // Y(culoare albastra)
            GL.Color3(Color.Blue);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(10, 100, 10);

            // Z(culoare neagra)
            GL.Color3(Color.Black);
            GL.Vertex3(10, 10, 10);
            GL.Vertex3(10, 10, 100);

    GL.End();
   Vertex-urile trebuie desenate în ordine antiorară. Pentru a realiza acest lucru, putem utiliza comanda "Begin" la începutul procesului de desenare a liniilor și comanda "End" la final, deoarece lucrăm cu aceeași categorie de primitive.
## 2. Anti-aliasing
`Anti-aliasing` folosește diverse tehnici pentru a elimina marginile zimțate de pe ecran care apar atunci când desenăm o linie diagonală pe un ecran cu poziționarea naturală a pixelilor fiind perpendiculară. Un exemplu de anti-aliasing implică escalarea imaginii cu un factor de scalare și apoi redimensionarea acesteia la dimensiunea sa inițială pentru a obține o claritate optimă.

## 3. GL.LineWidth(float) & GL.PointSize(float)
Comanda `GL.PointSize(float)` permite specificarea dimensiunii punctelor. În plus, comanda `GL.LineWidth(float)` ne oferă posibilitatea de a mări grosimea liniei. Trebuie să ținem cont că comanda `GL.PointSize(float)` nu funcționează în afara contextului `GL.Begin`.

## 6. Avantajul si importanta utilizarii culorii diferite in desenarea obiectelor 3D
Folosirea culorilor distincte sau a gradientelor pune în valoare mai eficient formele obiectelor și conferă un efect tridimensional (3D) mai pronunțat. Avantajul constă în obținerea unui efect vizual care evidențiază reliefurile și dimensiunile obiectelor, aducând astfel o diferență semnificativă în domeniul graficii.

## 7. Gradientul de Culoare
Un gradient reprezintă o gamă de culori care accentuează tranziția de la o nuanță la alta. Cu ajutorul OpenGL, putem obține acest efect prin definirea culorilor pentru vertex-urile care formează o anumită formă, astfel creându-se o trecere treptată de la un vertex la altul, generând un gradient.

## 8. Canalul de Transparenta
Această valoare reprezintă un număr pe 32 de biți, variind de la 0 (complet transparent) la 255 (complet opac).

## 10. Modul Strip
Un efect caracteristic de gradient va fi generat pe acea linie, evoluând de la un vertex la cel învecinat.   
