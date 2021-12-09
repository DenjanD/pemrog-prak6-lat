using System.Data;
using System.Data.SqlClient;

namespace P6_3_1204039
{
    public partial class Form1 : Form
    {
        string prodi;
        public Form1()
        {
            InitializeComponent();

            string myConnectionString = "integrated security=true;data source=.;initial catalog=P6_1204039";
            SqlConnection conn = new SqlConnection(myConnectionString); conn.Open();
            SqlCommand sc = new SqlCommand("SELECT * FROM msprodi", conn);
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("id_prodi", typeof(string));
            dt.Columns.Add("singkatan", typeof(string));
            dt.Load(reader);

            ProdiCB.ValueMember = "id_prodi";
            ProdiCB.DisplayMember = "singkatan";
            ProdiCB.DataSource = dt;

            conn.Close();
        }

        private void NPMTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NPMTextBox.TextLength < 7)
            {
                errorProvider1.SetError(NPMTextBox, "Format NPM belum benar!");
            } else if (NPMTextBox.TextLength == 7) {
                errorProvider1.SetError(NPMTextBox, "");
            } else if (NPMTextBox.TextLength == 0)
            {
                errorProvider1.SetError(NPMTextBox, "Tidak Boleh Kosong!");
            }
        }

        private void NamaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (NamaTextBox.TextLength == 0)
            {
                errorProvider1.SetError(NamaTextBox, "Tidak Boleh Kosong!");
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (NPMTextBox.Text != "" && NPMTextBox.TextLength == 7)
            {
                if (NamaTextBox.Text != "")
                {
                    if (TanggalLahirDT.Text != "")
                    {
                        if (LakiRB.Checked || PerempuanRB.Checked)
                        {
                            if (AlamatTextBox.Text != "")
                            {
                                if (NoTelpTextBox.Text != "")
                                {
                                    if (ProdiCB.Text != "- Pilih Jenis Kelamin -")
                                    {
                                        string npm = NPMTextBox.Text;
                                        string nama = NamaTextBox.Text;
                                        string ttl = TanggalLahirDT.Text;
                                        string jk = "";
                                        if (LakiRB.Checked)
                                        {
                                            jk = LakiRB.Text;
                                        }
                                        if (PerempuanRB.Checked)
                                        {
                                            jk = PerempuanRB.Text;
                                        }
                                        string alamat = AlamatTextBox.Text;
                                        string notelp = NoTelpTextBox.Text;
                                        string prodi = this.prodi;

                                        string myConnectionString = "integrated security=true;data source=.;initial catalog=P6_1204039";
                                        SqlConnection conn = new SqlConnection(myConnectionString);

                                        string sql = "insert into msmhs ([nim], [nama], [tgl_lahir], [jenis_kelamin], [alamat], " +
                                            "[telepon], [id_prodi]) values(@nim,@nama,@ttl,@jk,@alamat,@tlp,@idprodi)";

                                        using (SqlConnection cnn = new SqlConnection(myConnectionString))
                                        {
                                            try
                                            {
                                                cnn.Open();

                                                using (SqlCommand cmd = new SqlCommand(sql, cnn))
                                                {
                                                    cmd.Parameters.Add("@nim", SqlDbType.NVarChar).Value = npm;
                                                    cmd.Parameters.Add("@nama", SqlDbType.NVarChar).Value = nama;
                                                    cmd.Parameters.Add("@ttl", SqlDbType.NVarChar).Value = ttl;
                                                    cmd.Parameters.Add("@jk", SqlDbType.NVarChar).Value = jk;
                                                    cmd.Parameters.Add("@alamat", SqlDbType.NVarChar).Value = alamat;
                                                    cmd.Parameters.Add("@tlp", SqlDbType.NVarChar).Value = notelp;
                                                    cmd.Parameters.Add("@idprodi", SqlDbType.NVarChar).Value = prodi;

                                                    int rowsAdded = cmd.ExecuteNonQuery();
                                                    if (rowsAdded > 0)
                                                        MessageBox.Show("Data berhasil disimpan");
                                                    else
                                                        MessageBox.Show("Tidak ada data yang disimpan");

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("ERROR:" + ex.Message);
                                            }
                                        }

                                    } else
                                    {
                                        MessageBox.Show
                                                    ("Prodi belum diisi!",
                                                    "Informasi Data Submit",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                } else
                                {
                                    MessageBox.Show
                                                ("No Telp belum diisi!",
                                                "Informasi Data Submit",
                                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            } else
                            {
                                MessageBox.Show
                                            ("Alamat belum diisi!",
                                            "Informasi Data Submit",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        } else
                        {
                            MessageBox.Show
                                        ("Jenis Kelamin belum diisi!",
                                        "Informasi Data Submit",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    } else
                    {
                        MessageBox.Show
                                    ("Tanggal Lahir belum diisi!",
                                    "Informasi Data Submit",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } else
                {
                    MessageBox.Show
                                ("Nama belum diisi!",
                                "Informasi Data Submit",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show
                            ("NPM belum diisi!",
                            "Informasi Data Submit",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }    
        }

        private void ProdiCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.prodi = ProdiCB.SelectedValue.ToString();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            NPMTextBox.Text = null;
            NamaTextBox.Text = null;
            AlamatTextBox.Text = null;
            NoTelpTextBox.Text = null;
            ProdiCB.SelectedIndex = 0;
        }
    }
}