using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jogo_Da_Memoria
{
    public partial class Form1 : Form
    {
        private List<string> icons;
        private Random random;
        private int segundos;
        private Label firstClicked;
        private Label secondClicked;

        public Form1()
        {
            InitializeComponent();
            icons = new List<string>()
            {
                "♦","♦","♣","♣","♠","♠","♪","♪","♫","♫","☻","☻","ß","ß","¥","¥"
            };
            random = new Random();
            segundos = 0;
        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            IniciarJogo();
        }

        private void IniciarJogo()
        {
            foreach (Control control in Table.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (icons.Count > 0)
                    {
                        int randomNumber = random.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        icons.RemoveAt(randomNumber);
                    }
                }
            }

            TimerInicio.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ChecarVenceu()
        {
            foreach (Control control in Table.Controls)
            {
                Label iconLabel = control as Label;

                if(iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("VOCÊ VENCEU!", "Parabéns");
            Close();
        }

        private void LabelClick(object sender, EventArgs e)
        {
            if (TimerInicio.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if(clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                
                if(firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                ChecarVenceu();

                if(firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                TimerClick.Start();
            }
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TimerInicio_Tick(object sender, EventArgs e)
        {
            segundos++;

            if (segundos == 3)
            {
                foreach (Control control in Table.Controls)
                {
                    Label iconLabel = control as Label;
                    iconLabel.ForeColor = iconLabel.BackColor;
                }

                TimerInicio.Stop();
            }
        }

        private void TimerClick_Tick(object sender, EventArgs e)
        {
            TimerClick.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
    }
}
