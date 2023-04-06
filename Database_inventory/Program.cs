using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database : ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-A8KDLC9E;" + "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Hapus Data");
                                        Console.WriteLine("4. Update Data");
                                        Console.WriteLine("5. Keluar");
                                        Console.Write("\nEnter your choice (1-3) : ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA BARANG\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA BARANG\n");
                                                    Console.WriteLine("Masukkan ID Barang :");
                                                    string Id_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Barang :");
                                                    string Nama_Barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Banyak Barang :");
                                                    string banyak_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tipe Barang (Fragile/NotFragile) :");
                                                    string Tipe_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Deskripsi Barang :");
                                                    string Deskripsi_brg = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(Id_barang, Nama_Barang, banyak_barang, Tipe_barang, Deskripsi_brg, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DELETE DATA BARANG\n");
                                                    Console.WriteLine("Masukkan ID Barang yang akan dihapus:");
                                                    string Id_barang = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.delete(Id_barang, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menghapus data");
                                                    }
                                                }
                                                break;
                                            case '4':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("UPDATE DATA BARANG\n");
                                                    Console.WriteLine("Masukkan ID Barang yang akan diubah :");
                                                    string Id_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Barang yang baru :");
                                                    string Nama_Barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Banyak Barang yang baru :");
                                                    string banyak_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tipe Barang yang baru (Fragile/NotFragile) :");
                                                    string Tipe_barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Deskripsi Barang yang baru :");
                                                    string Deskripsi_brg = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.update(Id_barang, Nama_Barang, banyak_barang, Tipe_barang, Deskripsi_brg, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk mengubah data");
                                                    }
                                                }
                                                break;
                                            case '5':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }
    }
    }
}
