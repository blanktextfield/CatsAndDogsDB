﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace CatsAndDogsDB
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;   

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["CatsAndDogsDB.Properties.Settings.PetsConnectionString"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulatePetsTable();
        }
        private void PopulatePetsTable()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select * From petType", connection))
            {
                DataTable petTable = new DataTable();
                adapter.Fill(petTable);

                listPets.DisplayMember = "PetTypeName";
                listPets.ValueMember = "Id";
                listPets.DataSource = petTable;

            }


        }
        private void listPets_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePetNames();
        }

        private void PopulatePetNames()
        {
            string query = "Select Pet.Name From PetType Inner Join Pet ON Pet.TypeId = PetType.Id Where PetType.Id = @TypeId";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", listPets.SelectedValue);
                DataTable petNameTable = new DataTable();
                adapter.Fill(petNameTable);

                listPetNames.DisplayMember = "Name";
                listPetNames.ValueMember = "Id";
                listPetNames.DataSource = petNameTable;
            }

        }


        
    }
}
