using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
                                        Console.Write("\nEnter your choice (1-4) : ");
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

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From barang", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        public void insert(string Id_barang, string Nama_barang, string banyak_barang, string Tipe_barang, string Deskripsi_brg, SqlConnection con)
        {
            string str = "";
            str = "insert into barang (Id_barang, Nama_barang, banyak_barang, Tipe_barang, Deskripsi_brg)values(@id, @nma, @byk, @tp, @dsk)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", Id_barang));
            cmd.Parameters.Add(new SqlParameter("nma", Nama_barang));
            cmd.Parameters.Add(new SqlParameter("byk", banyak_barang));
            cmd.Parameters.Add(new SqlParameter("tp", Tipe_barang));
            cmd.Parameters.Add(new SqlParameter("dsk", Deskripsi_brg));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }

        public void delete(string Id_barang, SqlConnection con)
        {
            string str = "";
            str = "delete from barang where Id_barang = @id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", Id_barang));
            int affectedRows = cmd.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                Console.WriteLine("Data dengan Id_barang " + Id_barang + " berhasil dihapus");
            }
            else
            {
                Console.WriteLine("Data dengan Id_barang " + Id_barang + " tidak ditemukan");
            }
        }

        public void update(string Id_barang, string Nama_barang, string banyak_barang, string Tipe_barang, string Deskripsi_brg, SqlConnection con)
        {
            string str = "";
            str = "UPDATE barang SET Nama_barang = @nma, banyak_barang = @byk, Tipe_barang = @tp, Deskripsi_brg = @dsk WHERE Id_barang = @id";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id", Id_barang));
            cmd.Parameters.Add(new SqlParameter("nma", Nama_barang));
            cmd.Parameters.Add(new SqlParameter("byk", banyak_barang));
            cmd.Parameters.Add(new SqlParameter("tp", Tipe_barang));
            cmd.Parameters.Add(new SqlParameter("dsk", Deskripsi_brg));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Diubah");
        }

    }
}