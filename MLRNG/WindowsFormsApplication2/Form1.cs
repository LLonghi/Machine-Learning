using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Dictionary<string, string> Dicionario = new Dictionary<string, string>();//Antigo|novo

        private static List<string> wordsToRemove =
       "DE DA DAS DO DOS NAS NO NOS EM E A AS O OS AO AOS".Split(' ').ToList();

        public static string StringWordsRemove(string stringToClean)
        {
            // Define how to tokenize the input string, i.e. space only or punctuations also
            return string.Join(" ", stringToClean
                .Split(new[] { ' ', ',', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .Except(wordsToRemove));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Pesquisa = txtPesquisa.Text.ToUpper();
            string Retorno = "";
            bool Existe = false;
            foreach (ListViewItem inTable in lvwValores.Items)
            {
                if (!Existe)
                {
                    bool grava = false;
                    bool isSame = false;
                    string pesq = Pesquisa, val = inTable.Text.ToUpper();
                    isSame = !pesq.Except(val).Any() && !val.Except(pesq).Any();
                    Log.Text += "┌─────Verificação:  | "+inTable.Text+"\r\n│\r\n";
                    if (!isSame)
                    {
                        grava = true;
                        string x = "";
                        if (Dicionario.TryGetValue(pesq, out x))
                        {
                            pesq = x;
                            isSame = !pesq.Except(val).Any() && !val.Except(pesq).Any();
                        }
                        Log.Text += "│ 1|(" + pesq + " == " + val + ") - " + isSame + "\r\n";

                        if (!isSame)
                        {
                            pesq = StringWordsRemove(Pesquisa);
                            isSame = !pesq.Except(val).Any() && !val.Except(pesq).Any();
                            Log.Text += "│ 2|(" + pesq + " == " + val + ") - " + isSame + "\r\n";

                            if (!isSame)
                            {
                                pesq = StringWordsRemove(Pesquisa);
                                val = StringWordsRemove(val);
                                isSame = !pesq.Except(val).Any() && !val.Except(pesq).Any();
                                Log.Text += "│ 3|(" + pesq + " == " + val + ") - " + isSame + "\r\n";


                                if (!isSame)
                                    Log.Text += "│ 0|(" + pesq + " == " + val + ") - " + isSame + "\r\n";
                            }
                        }
                    }
                    else
                        Log.Text += "│ -|" + pesq + " <- (" + Pesquisa + " == " + val + ") - " + isSame + "\r\n";

                    Log.Text += "│ \r\n└──";
                    Log.Text += Pesquisa + " == "+inTable.Text+" ? " + isSame;
                    Log.Text += "\r\n\r\n";

                   

                    if (isSame)
                    {
                        if (grava)
                        {
                            add_dicionario(inTable.Text);
                        }
                        Retorno = inTable.Text;
                        Existe = true;
                        Log.Text += "Pesquisa Existente. " + Pesquisa + " = " + inTable.Text + "\r\n\r\n";
                        lblEq.Text = "Equivalente: " + Retorno;
                        txtHistorico.Text += Pesquisa+":"+Existe + "\r\n";
                    }
                }
            }
            if (!Existe)
            {
                txtHistorico.Text += Pesquisa + ":" + Existe + "\r\n";
                Log.Text += "Pesquisa não Existente.\r\n";
            }

            Log.Text += "---------------------------------------------------------------------------------------------------------\r\n\r\n";

            lblEx.Text = "Existe: " + Existe;
            lblPe.Text = "Pesquisa: " + Pesquisa;
        }

        private void add_dicionario(string x)
        {
            try
            {
                Dicionario.Add(txtPesquisa.Text.ToUpper(), x);
                cDicionario.Settings.Instance.Parameters = Dicionario;
                cDicionario.Settings.Instance.Save();
                cDicionario.Settings.Instance.Reload();
                atualiza_dicionario();
            }
            catch { }

        }

        private void atualiza_dicionario()
        {
            lvwDicionario.Items.Clear();
            lvwDicionario.View = View.Details;

            foreach (var Item in Dicionario)
            {
                string[] row = { Item.Key, Item.Value };
                var listViewItem = new ListViewItem(row);
                lvwDicionario.Items.Add(listViewItem);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Dicionario = cDicionario.Settings.Instance.Parameters;

            if (Dicionario == null)
                Dicionario = new Dictionary<string, string>();

            atualiza_dicionario();

            lvwDicionario.View = View.Details;

            lvwValores.Items.Add("QUALQUER COISA");
            lvwValores.Items.Add("BENEFICIAMENTO DE RETORNO");
            lvwValores.Items.Add("TESTE TESTER");
            lvwValores.Items.Add("DESCONTO 50");
        }

        private void lblEx_Click(object sender, EventArgs e)
        {

        }

        private void lblEq_Click(object sender, EventArgs e)
        {

        }

        private void lblPe_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Log.Clear();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
