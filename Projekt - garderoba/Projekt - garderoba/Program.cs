using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt___garderoba
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sytuacja , gdzie Marek i Stanisław będą się ubierać 
            Garderoba Garderoba1 = new Garderoba(1);
            Osoba Stanislaw = new Osoba(1, "Stanisław");
            Osoba Marek = new Osoba(1, "Marek");
            Garderoba1.Wejdz(Stanislaw);//Osoba wchodzi do garderoby nr 1
            Garderoba1.Powiadom();
            Garderoba1.Wejdz(Marek);//Marek chce wejść do garderony nr 1 , ale ona jest zajęta , więc musi poczekać
            Console.WriteLine("Stanisław ubiera: ");
            Ubranie bluza1 = new Bluza();
            bluza1 = new RozpinanaBluza(bluza1);
            bluza1 = new UkolorSzary(bluza1);
            Console.WriteLine(bluza1.Opis() + bluza1.Rozmiar());
            Ubranie spodnie1 = new Spodnie();
            spodnie1 = new Dresy(spodnie1);
            spodnie1 = new UkolorCzarny(spodnie1);
            Console.WriteLine(spodnie1.Opis() + spodnie1.Rozmiar());
            Buty buty1 = new Sportowe(); // tworzenie butów 
            buty1 = new Nike(buty1); // dodanie , że sa one Nike
            buty1 = new BkolorBialy(buty1); // dodanie , że są czarne
            Console.WriteLine(buty1.Rodzaj() + buty1.RozmiarB()); // wypisanie , że Stanisał zakłada te buty
            Garderoba1.Wyjdz(Stanislaw);
            Garderoba1.Powiadom();
            Garderoba1.Wejdz(Marek);//Po opuszczeniu garderoby przez Stanisława, Marek może wejśc do garderoby nr 1 
            Garderoba1.Powiadom();
            Console.WriteLine("\r\nMarek ubiera: ");
            Ubranie koszula1 = new Koszula();
            koszula1 = new UkolorBialy(koszula1);
            koszula1 = new ZeStojka(koszula1);
            Console.WriteLine(koszula1.Opis() + koszula1.Rozmiar());
            Ubranie spodnie2 = new Spodnie();
            spodnie2 = new Garniturowe(spodnie2);
            spodnie2 = new UkolorSzary(spodnie2);
            Console.WriteLine(spodnie2.Opis() + spodnie2.Rozmiar());
            Buty buty2 = new Eleganckie();
            buty2 = new Lasocki(buty2);
            buty2 = new BkolorCzarny(buty2);
            Console.WriteLine(buty2.Rodzaj() + buty2.RozmiarB());



            Console.ReadKey();
        }
    }
    public interface IObserwator
    {
        void Update();
    }
    public interface IUżytkownik
    {
        void Wejdz(IObserwator obserwator);
        void Wyjdz(IObserwator obserwator);
        void Powiadom();
    }
    public class Garderoba : IUżytkownik
    {
        private List<IObserwator> Korzystający; // Lista osób korzystających z garderoby 
        public int numer;
        public Garderoba(int numer)
        {
            this.numer = numer;
            Korzystający = new List<IObserwator>(); // Przy utworzeniu nowej garderoby tworzy sie lista osób , w której będzie osoba korzystająca z niej
        }
        public void Wejdz(IObserwator obserwator)
        {
            if(Korzystający.Count <1)
            {
                Korzystający.Add(obserwator); // w jednej garderobie powinna ubierac/przebierac sie jedna osoba
            }
            else
            {
                Console.WriteLine("Jeśli chcesz skorzystać z garderoby numer " +this.numer +  ", proszę chwilę poczekać... \r\n");
                
            }
        }
        public void Wyjdz(IObserwator obserwator)
        {            
            Korzystający.Remove(obserwator);// Osoba opuszcza garderobę 
            Console.WriteLine("\r\nGarderoba numer " + this.numer + " jest wolna");
        }
        public void Powiadom() //Mówi innym oczekującym , że ta garderoba jest przez kogoś zajęta 
        {
            foreach(IObserwator obserwator in Korzystający)
            {
                obserwator.Update();
            }
        }
    }
    public class Osoba : Garderoba,  IObserwator
    {
        private string name;
        public Osoba(int numer, string name) : base(numer)
        {
            this.name = name;
            this.numer = numer;
        }

        public void Update()
        {
            Console.WriteLine("\r\nOsoba o imieniu " + this.name + " wchodzi do garderoby o numerze " + this.numer); //osoba dostaje wiadomosc , że ktoś już jest w tej garderobie
        }
    }
    abstract public class Ubranie
    {
        public abstract string Opis();
        public abstract string Rozmiar();

    }

    public abstract class UbranieDekorator : Ubranie
    {
        protected Ubranie clothes;
        protected UbranieDekorator(Ubranie _clothes)
        {
            this.clothes = _clothes;
        }
        public override string Opis()
        {
            return clothes.Opis();
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar();
        }
    }
    abstract public class Buty
    {
        public abstract string Rodzaj();
        public abstract string RozmiarB();
    }
    public abstract class ButyDekorator : Buty
    {
        protected Buty shoes;
        protected ButyDekorator(Buty _shoes)
        {
            this.shoes = _shoes;
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj();
        }
        public override string RozmiarB()
        {
            return shoes.RozmiarB();
        }
    }

    public class Spodnie : Ubranie
    {
        public override string Opis()
        {
            return "Spodnie";
        }
        public override string Rozmiar()
        {
            return " w rozmiarze";
        }
    }
    public class Dresy : UbranieDekorator
    {
        public Dresy(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " dresowe";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + " L";
        }
    }
    public class Garniturowe : UbranieDekorator
    {
        public Garniturowe(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " garniturowe";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + " L";
        }
    }
    public class Koszula : Ubranie
    {
        public override string Opis()
        {
            return "Koszulę";
        }
        public override string Rozmiar()
        {
            return " w rozmiarze";
        }
    }
    public class ZeStojka : UbranieDekorator
    {
        public ZeStojka(Ubranie _clothes) : base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + " ze stójką";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + " M";
        }
    }
    public class Koszulka : Ubranie
    {
        public override string Opis()
        {
            return "Koszulkę";
        }
        public override string Rozmiar()
        {
            return " w rozmiarze";
        }
    }
    public class Polo : UbranieDekorator
    {
        public Polo(Polo _clothes) : base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + " polo";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + " L";
        }
    }
    public class Bluza : Ubranie
    {
        public override string Opis()
        {
            return "Bluzę";
        }
        public override string Rozmiar()
        {
            return " w rozmiarze";
        }
    }
    public class RozpinanaBluza : UbranieDekorator
    {
        public RozpinanaBluza(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " rozpinaną";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + " L";
        }
    }
    public class UkolorBialy : UbranieDekorator
    {
        public UkolorBialy(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " w kolorze białym";
        }
    }
    public class UkolorCzarny : UbranieDekorator
    {
        public UkolorCzarny(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " w kolorze czarnym";
        }
    }
    public class UkolorSzary : UbranieDekorator
    {
        public UkolorSzary(Ubranie _clothes) : base(_clothes)
        {
        }

        public override string Opis()
        {
            return clothes.Opis() + " w kolorze szarym";
        }
    }
    public class Sportowe : Buty
    {
        public override string Rodzaj()
        {
            return "Sportowe buty";
        }
        public override string RozmiarB()
        {
            return " w rozmiarze";
        }
    }
    public class Eleganckie : Buty
    {
        public override string Rodzaj()
        {
            return "Eleganckie buty";
        }
        public override string RozmiarB()
        {
            return " w rozmiarze";
        }
    }
    public class Nike : ButyDekorator
    {
        public Nike(Buty _shoes) : base(_shoes)
        {
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj() + " marki Nike";
        }
        public override string RozmiarB()
        {
            return shoes.RozmiarB() + " 42,5";
        }
    }
    public class Adidas : ButyDekorator
    {
        public Adidas(Buty _shoes) : base(_shoes)
        {
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj() + " marki Adidas";
        }
        public override string RozmiarB()
        {
            return shoes.RozmiarB() + " 42";
        }
    }
    public class Lasocki : ButyDekorator
    {
        public Lasocki(Buty _shoes) : base(_shoes)
        {
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj() + " marki Lasocki";
        }
        public override string RozmiarB()
        {
            return shoes.RozmiarB() + " 42";
        }
    }
    public class BkolorBialy : ButyDekorator
    {
        public BkolorBialy(Buty _shoes) : base(_shoes)
        {
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj() + " w kolorze białym";
        }

    }
    public class BkolorCzarny : ButyDekorator
    {
        public BkolorCzarny(Buty _shoes) : base(_shoes)
        {
        }
        public override string Rodzaj()
        {
            return shoes.Rodzaj() + " w kolorze czarnym";
        }

    }

} 
