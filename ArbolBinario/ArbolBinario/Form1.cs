using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbolBinario
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        Nodo raiz;
        Nodo seleccionado;

        Nodo crearNodo()
        {
            string nombre = Interaction.InputBox("Ingrese nombre del nodo");
            return new Nodo(nombre);
        }

        void EvaluarArbol()
        {
            this.lblAltura.Text = $"Altura:{Alto(raiz)}";
            int inicio = 0;
            this.lblAncho.Text = $"Ancho:{Ancho(raiz, ref inicio)}";
        }

        int Ancho(Nodo n, ref int ancho)
        {
            if (n.Derecha == null && n.Izquierda == null)
                ancho += 1;
            if (n.Derecha != null) Ancho(n.Derecha, ref ancho);
            if (n.Izquierda != null) Ancho(n.Izquierda, ref ancho);

            return ancho;
        }
        int Alto(Nodo n)
        {
            if (n == null) return 0;

            int izq = Alto(n.Izquierda) + 1;
            int der = Alto(n.Derecha) + 1;
            return Math.Max(izq, der);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (raiz != null)
            {
                DialogResult r = MessageBox.Show("Se eliminará el árbol desea continuar?", "consulta", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    raiz = crearNodo();
                }
            }
            else
            {
                raiz = crearNodo();
            }
            CambiarSeleccion(raiz);
            LlenarTreeView();
            
        }

        void CambiarSeleccion(Nodo n)
        {
            seleccionado = n;
            this.lblNombreNodo.Text = n.Nombre;
        }

        public void LlenarTreeView()
        {
            treeView1.Nodes.Clear();
            MostrarNodo(raiz, null, string.Empty);
            treeView1.ExpandAll();
            EvaluarArbol();
        }

        public void MostrarNodo(Nodo n, TreeNode tnpadre, string lado)
        {
            if (n == null) return;

            TreeNode nuevo = new TreeNode();
            if (tnpadre == null && lado == string.Empty)
            {
                tnpadre = new TreeNode();
                nuevo.Text = n.Nombre;
                nuevo.Tag = n;
                treeView1.Nodes.Add(nuevo);
            }
            else
            {
                nuevo.Text = $"{lado} - {n.Nombre}";
                nuevo.Tag = n;

                tnpadre.Nodes.Add(nuevo);

            }

            if (n.Derecha != null) MostrarNodo(n.Derecha, nuevo, "Derecha");
            if (n.Izquierda != null) MostrarNodo(n.Izquierda, nuevo, "Izquierda");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (seleccionado != null)
            {
                seleccionado.Derecha = crearNodo();
                LlenarTreeView();
            }
            else
                MessageBox.Show("Debe tener algun nodo seleccionado");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.txtRecorrido.Text = string.Empty;
            RecorridoPreOrden(raiz);
        }

        void RecorridoPreOrden(Nodo n)
        {
            Visitar(n);
            if (n.Izquierda != null) RecorridoPreOrden(n.Izquierda);
            if (n.Derecha != null) RecorridoPreOrden(n.Derecha);
        }

        void RecorridoInreOrden(Nodo n)
        {
            if (n.Izquierda != null) RecorridoInreOrden(n.Izquierda);
            Visitar(n);
            if (n.Derecha != null) RecorridoInreOrden(n.Derecha);
        }

        void RecorridoPostreOrden(Nodo n)
        {
            if (n.Izquierda != null) RecorridoPostreOrden(n.Izquierda);
            if (n.Derecha != null) RecorridoPostreOrden(n.Derecha);
            Visitar(n);
        }

        void Visitar(Nodo n)
        {
            this.txtRecorrido.Text += "-" + n.Nombre;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.txtRecorrido.Text = string.Empty;
            RecorridoInreOrden(raiz);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.txtRecorrido.Text = string.Empty;
            RecorridoPostreOrden(raiz);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (seleccionado != null)
            {
                seleccionado.Izquierda = crearNodo();
                LlenarTreeView();
            }
            else
                MessageBox.Show("Debe tener algun nodo seleccionado");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CambiarSeleccion((Nodo)e.Node.Tag);
        }
    }
}
