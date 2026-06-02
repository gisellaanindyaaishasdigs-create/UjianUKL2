using System.Runtime.CompilerServices;

List<Stand> data_Stand = new List<Stand>();

{
    new StandOudoor("Stand Outdoor 1", 400000);
    new StandOudoor("Stand Outdoor 2", 500000);
    new StandIndoor("Stand Indoor 1", 700000);
    new StandIndoor("Stand Indoor 2", 800000);
    new StandPremium("Stand Premium 1", 1800000);
    new StandPremium("Stand Premium 2", 2000000);
}

while (true)
{
    Console.WriteLine("---Moklet Expo Management Center---");
    foreach (var ds in data_Stand )
    {
        ds.TampilkanInfo();
    }

    Console.WriteLine("Pilih menu: ");
    Console.WriteLine("1. sewa\n2.kembali\n3. keluar");
    Console.Write("Pilih menu: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        Console.Write("\nInput nama Stand");
        string nama_Stand = Console.ReadLine();

        var cari_Stand = data_Stand.FirstOrDefault(ck => string.Equals(ck.NamaStand, nama_Stand, StringComparison.OrdinalIgnoreCase));

        if (cari_Stand == null)
        {
            Console.WriteLine("Stand tidak di temukan");
        }

        else if (cari_Stand.IsAvailable)
        {
            Console.Write("\nInput jumlah hari sewa");
            int hari = int.Parse(Console.ReadLine());

            double totalBiaya = cari_Stand.HitungTotal(hari);

            cari_Stand.UbahStatus();

            Console.WriteLine($"Total pembayaran sewa: Rp {totalBiaya}");
        }



    }
    if (pilihan == "2")
    {
        Console.WriteLine("\nDaftar Stand:");
        string namaStand = Console.ReadLine();

        var cari_Stand = data_Stand.FirstOrDefault(ck => string.Equals(ck.NamaStand, namaStand, StringComparison.OrdinalIgnoreCase));


        if (cari_Stand == null)
        {
            Console.WriteLine("Stand tidak ditemukan");
        }

        else if (!cari_Stand.IsAvailable)
        {
            cari_Stand.UbahStatus();

            Console.WriteLine("\nStand berhasil dikembalikan!");
        }
        else
        {
            Console.WriteLine("\nProses pengembalian tidak bisa dilakukan");
        }

    }


    else if (pilihan == "3")
    {
        Console.WriteLine("Terima Kasih");
        break;
    }

    else
    {
        Console.WriteLine("\nPilihan invalid");
    }
    Console.WriteLine("Tekan ENTER untuk menutup Aplikasi....");
    Console.ReadLine();
}




class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerhari;
    protected bool _isAvailable;
    

    public Stand(string namaStand, double hargaSewaPerhari)
    {
        _namaStand = namaStand;
        _hargaSewaPerhari = hargaSewaPerhari;
        _isAvailable = true;
    }

    public string NamaStand
    {
        get { return _namaStand; }
        set { _namaStand = value; }
    }

    public double HargaSewaPerhari
    {
        get { return _hargaSewaPerhari; }
        set
        {
            if (value < 0)
            {
                _hargaSewaPerhari = value;
            }


            else
            {
                Console.WriteLine("Harga sewa nya harus lebih dari nol");
            }

        }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
        set { _isAvailable = value; }
    }

    public void TampilkanInfo()
    {
        Console.WriteLine($"Nama Stand: {_namaStand}");
        Console.WriteLine($"Harga Sewa Perhari: {_hargaSewaPerhari}");
        Console.WriteLine($"{_namaStand} | Rp {_hargaSewaPerhari} / hari | {(_isAvailable ? "Tersedia" : "Tidak Tersedia")}");
    }
        

    public void UbahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public double HitungTotal(int jumlahHari)
    {
        return _hargaSewaPerhari * jumlahHari;
    }

    
}

class StandIndoor : Stand
{
    private double _biayaListrik;

    public StandIndoor(string namaStand, double hargaSewaPerhari) : base(namaStand, hargaSewaPerhari)
    {
        _biayaListrik = 100000;
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        _biayaListrik = _biayaListrik * jumlahHari;
        return base.HitungTotal(jumlahHari) + _biayaListrik;
    }
}


class StandOudoor : Stand
{
    private double _biayaTenda;

    public StandOudoor(string namaStand, double hargaSewaPerhari) : base(namaStand, hargaSewaPerhari)
    {
        _biayaTenda = 75000;
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        _biayaTenda = _biayaTenda * jumlahHari;
        return base.HitungTotal(jumlahHari) + _biayaTenda;
    }

    
}

class StandPremium : Stand
{
    private double _biayaKeamanan;
    public StandPremium(string namaStand, double hargaSewaPerhari) : base(namaStand, hargaSewaPerhari)
    {
        _biayaKeamanan = 30000;
    }
    public virtual double HitungTotal(int jumlahHari)
    {
        _biayaKeamanan = _biayaKeamanan * jumlahHari;
        return base.HitungTotal(jumlahHari) + _biayaKeamanan;
    }
}

