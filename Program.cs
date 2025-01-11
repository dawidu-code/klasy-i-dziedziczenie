namespace KlasyIDziedziczenie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ksiazka> ksiazki = new List<Ksiazka>
            {
                new Ksiazka("fantasy", 1999),
                new Ksiazka("horror", 2013),
                new Ksiazka("science fiction", 2001),
                new Ksiazka("thriller", 2018),
                new Ksiazka("romans", 2010),
                new Ksiazka("mystery", 2005),
                new Ksiazka("biografia", 1995),
                new Ksiazka("historyczna", 1980),
                new Ksiazka("przygodowa", 2020),
                new Ksiazka("psychologia", 2015)
            };

            Sklep mojSklep = new Sklep(3);
            int userChoice = 1;

            // Dodanie klienta
            Koszyk koszykPawla = new Koszyk([new Produkt("Odyseja", 15.40, ksiazki[1]), new Produkt("Wiersze", 9.50, ksiazki[6]), new Produkt("Tomb Rider", 40.20, ksiazki[9])]);
            Klient klient = new Klient("Paweł", 1, koszykPawla);
            mojSklep.DodajKlienta(klient);

            while (userChoice != 0)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("      Witaj w Systemie Sklepu          ");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Dodaj Książkę");
                Console.WriteLine("2. Wyświetl Książki");
                Console.WriteLine("3. Dodaj Produkt");
                Console.WriteLine("4. Dodaj Klienta");
                Console.WriteLine("5. Wyświetl Sklep");
                Console.WriteLine("0. Wyjście");
                Console.WriteLine("=========================================");
                Console.Write("Wybierz opcję: ");
                userChoice = int.TryParse(Console.ReadLine(), out var choice) ? choice : 9;

                switch (userChoice)
                {
                    case 1:
                        ksiazki.Add(DodajKsiazke());
                        break;
                    case 2:
                        WyświetlKsiazki(ksiazki);
                        break;
                    case 3:
                        DodajProdukt();
                        break;
                    case 4:
                        DodajKlientaDoSklepu(mojSklep);
                        break;
                    case 5:
                        WyświetlStatus(mojSklep);
                        break;
                }

                Console.WriteLine("Naciśnij dowolny guzik...");
                Console.ReadKey();
            }
        }


            static void WyświetlKsiazki(List<Ksiazka> ksiazki)
        {
            foreach( Ksiazka ksiazka in ksiazki)
            {
                ksiazka.podajInfo();
            }
        }

        static Ksiazka DodajKsiazke()
        {
            Console.Write("Podaj gatunek: ");
            string gatunek = Console.ReadLine();

            Console.Write("Podaj rok wydania: ");
            int rokWydania;
            while (!int.TryParse(Console.ReadLine(), out rokWydania))
            {
                Console.Write("Wprowadź poprawny rok wydania: ");
            }

            return new Ksiazka(gatunek, rokWydania);

        }

        static Produkt DodajProdukt()
        {
            Console.Write("Podaj gatunek: ");
            string gatunek = Console.ReadLine();

            Console.Write("Podaj rok wydania: ");
            int rokWydania;
            while (!int.TryParse(Console.ReadLine(), out rokWydania))
            {
                Console.Write("Wprowadź poprawny rok wydania: ");
            }

            Console.Write("Podaj nazwę produktu: ");
            string nazwa = Console.ReadLine();

            Console.Write("Podaj cenę: ");
            double cena;
            while (!double.TryParse(Console.ReadLine(), out cena))
            {
                Console.Write("Wprowadź poprawną cenę: ");
            }

            return new Produkt(nazwa, cena, new Ksiazka(gatunek, rokWydania));
        }

        static void DodajKlientaDoSklepu(Sklep sklep)
        {
            Console.Write("Podaj imię klienta: ");
            string imie = Console.ReadLine();
            Console.Write("Podaj numer klienta: ");
            int numerKlienta = int.Parse(Console.ReadLine());

            // Tworzymy nowego klienta i dodajemy go do sklepu
            Klient nowyKlient = new Klient(imie, numerKlienta, new Koszyk(new Produkt[] { }));
            sklep.DodajKlienta(nowyKlient);
        }

        static void WyświetlStatus(Sklep sklep)
        {
            sklep.wyswietlInfoSklepu();
        }

        public class Ksiazka
        {
            public string Gatunek { get; set; }
            public int RokWydania { get; set; }

            public Ksiazka(string gatunek, int rokWydania)
            {
                Gatunek = gatunek;
                RokWydania = rokWydania;
            }
            public void podajInfo()
            {
                Console.WriteLine($"Gatunek ksiażki: {Gatunek}, Rok wydania: {RokWydania}");

            }
        }

        public class Produkt : Ksiazka
        {
            public string Nazwa { get; set; }
            public double Cena { get; set; }

            public Produkt(string nazwa, double cena, Ksiazka ksiazka)
                : base(ksiazka.Gatunek, ksiazka.RokWydania)
            {
                Nazwa = nazwa;
                Cena = cena;
            }
        }

        public class Koszyk
        {
            public List<Produkt> ListaProduktów { get; set; }
            public double SumaCeny;

            public Koszyk(Produkt[] produkty)
            {
                ListaProduktów = new List<Produkt>(produkty);
                SumaCeny = 0;

                foreach (Produkt produkt in ListaProduktów)
                {
                    SumaCeny += produkt.Cena;
                }
            }

            public void WypiszKoszyk()
            {
                foreach (Produkt produkt in ListaProduktów)
                {
                    Console.WriteLine($"    - {produkt.Nazwa} (Cena: {produkt.Cena})");
                }
            }

            public void DodajProdukt(Produkt produkt)
            {
                ListaProduktów.Add(produkt);
                SumaCeny += produkt.Cena;
            }
        }

        public class Klient
        {
            public string Imie { get; set; }
            public int NumerKlienta { get; set; }
            public Koszyk Koszyk { get; set; }

            public Klient(string imie, int numerKlienta, Koszyk koszyk)
            {
                Imie = imie;
                NumerKlienta = numerKlienta;
                Koszyk = koszyk;
            }

            public void WyswietlInformacje()
            {
                Console.WriteLine($"Imię klienta: {Imie}, Numer klienta: {NumerKlienta}");
            }
        }

        public class Sklep
        {
            private List<Klient> klienci;
            private int maksymalnaLiczbaKlientow;

            public Sklep(int maksymalnaLiczbaKlientow)
            {
                this.maksymalnaLiczbaKlientow = maksymalnaLiczbaKlientow;
                klienci = new List<Klient>();
            }

            public void DodajKlienta(Klient nowyKlient)
            {
                if (klienci.Count < maksymalnaLiczbaKlientow)
                {
                    klienci.Add(nowyKlient);
                    Console.WriteLine($"Klient {nowyKlient.Imie} został dodany do sklepu.");
                }
                else
                {
                    Console.WriteLine("Sklep jest pełny. Nie można dodać nowego klienta.");
                }
            }

            public void DodajProduktDoKoszyka(int numerKlienta, Produkt produkt)
            {
                Klient klient = klienci.Find(k => k.NumerKlienta == numerKlienta);
                if (klient != null)
                {
                    klient.Koszyk.DodajProdukt(produkt);
                    Console.WriteLine($"Produkt {produkt.Nazwa} dodany do koszyka klienta {klient.Imie}.");
                }
                else
                {
                    Console.WriteLine("Klient nie istnieje.");
                }
            }

            public void wyswietlInfoSklepu()
            {
                Console.WriteLine("=========================================");
                Console.WriteLine("   Klienci i ich Koszyki w Sklepie    ");
                Console.WriteLine("=========================================");
                foreach (var klient in klienci)
                {
                    Console.WriteLine($"Imię klienta: {klient.Imie}, Numer klienta: {klient.NumerKlienta}");
                    Console.WriteLine("  Koszyk:");
                    klient.Koszyk.WypiszKoszyk();
                    Console.WriteLine($"  Suma cen w koszyku: {klient.Koszyk.SumaCeny}");
                    Console.WriteLine("=========================================");
                }
            }
        }
    }
}
