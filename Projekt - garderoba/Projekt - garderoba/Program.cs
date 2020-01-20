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
            Ubranie buty1 = new Buty(); // tworzenie butów 
            buty1 = new Nike(buty1); // dodanie , że sa one Nike
            buty1 = new CzarneButy(buty1); // dodanie , że są czarne
            Console.WriteLine(buty1.Opis()+ " o rozmiarze " + buty1.Rozmiar()); // wypisanie , że Stanisał zakłada te buty
            Garderoba1.Wyjdz(Stanislaw);
            Garderoba1.Wejdz(Marek);//Po opuszczeniu garderoby przez Stanisława, Marek może wejśc do garderoby nr 1 
            Garderoba1.Powiadom();
            Console.WriteLine("Marek ubiera ");
            Ubranie buty2 = new Buty();
            buty2 = new Adidas(buty2);
            buty2 = new BialeButy(buty2);
            Console.WriteLine(buty2.Opis() + " o rozmiarze " + buty2.Rozmiar());



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
                Console.WriteLine("Ktoś korzysta już z garderoby numer" +this.numer +  ", prosze poczekać ");
                
            }
        }
        public void Wyjdz(IObserwator obserwator)
        {
            Korzystający.Remove(obserwator);// Osoba opuszcza garderobę 
            Console.WriteLine("Garderoba numer " + this.numer + " jest wolna");
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
            Console.WriteLine("Osoba o imieniu " + this.name + " wchodzi do garderoby u numerze " + this.numer); //osoba dostaje wiadomosc , że ktoś już jest w tej garderobie
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
    public class Buty : Ubranie
    {
        public override string Opis()
        {
            return "Buty";
        }
        public override string Rozmiar()
        {
            return "";
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
            return "";
        }
    }
    public class Koszulka : Ubranie
    {
        public override string Opis()
        {
            return "Koszulka";
        }
        public override string Rozmiar()
        {
            return "";
        }
    }
    public class Bluza : Ubranie
    {
        public override string Opis()
        {
            return "Bluza";
        }
        public override string Rozmiar()
        {
            return "";
        }
    }
    public class CzarneButy : UbranieDekorator
    {
        public CzarneButy(Ubranie _clothes) : base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + ", kolor czarny";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + "42";
        }
    }
    public class Nike : UbranieDekorator
    {
        public Nike(Ubranie _clothes) : base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + " marki Nike";
        }
    }
    public class Adidas : UbranieDekorator
    {
        public Adidas(Ubranie _clothes): base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + " marki Adidas";
        }
    }
    public class BialeButy : UbranieDekorator
    {
        public BialeButy(Ubranie _clothes) : base(_clothes)
        {
        }
        public override string Opis()
        {
            return clothes.Opis() + ", kolor biały";
        }
        public override string Rozmiar()
        {
            return clothes.Rozmiar() + "45";
        }
    }

} 
