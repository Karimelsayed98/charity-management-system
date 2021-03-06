﻿using charity_management_system.Models;
using charity_management_system.Utils;
using System;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Collections.Generic;

namespace charity_management_system.Repositories
{
    class DonorRepo : IRepository<Donor>
    {
        private OracleConnection connection;
        private OracleCommand command;

        public DonorRepo()
        {
            connection = DBManager.instance.connection;
            command = new OracleCommand();
            command.Connection = connection;
        }
        public bool delete(Donor model)
        {
            command.CommandText = "delete from Donor where id =:donor_id";
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("donor_id", model.id);
            int check = command.ExecuteNonQuery();
            
            if(check == -1)
            {
                return false;
            }
            return true;
        }

        public List<Donor> find(string column, string value)
        {
            command.CommandText = "select * from donor where :columnName =:value";
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("columnName", column);
            command.Parameters.Add("value", value);

            OracleDataReader reader = command.ExecuteReader();
            List<Donor> Donors = new List<Donor>();
            while (reader.Read())
            {
                Donor donor = new Donor(
                                        reader["name"].ToString(),
                                        reader["mobile"].ToString(),
                                        reader["address_line1"].ToString(),
                                        reader["city"].ToString(),
                                        reader["governorate"].ToString());
                donor.id =int.Parse(reader["id"].ToString());
                donor.addressLine2 = reader["address_line2"].ToString();
                Donors.Add(donor);
            }
            reader.Close();
            return Donors;

        }

        public List<Donor> findAll()
        {
            command.CommandText = "select * from donor";
            command.CommandType = System.Data.CommandType.Text;

            OracleDataReader reader = command.ExecuteReader();
            List<Donor> Donors = new List<Donor>();
            while (reader.Read())
            {
                Donor donor = new Donor(
                                        reader["name"].ToString(),
                                        reader["mobile"].ToString(),
                                        reader["address_line1"].ToString(),
                                        reader["city"].ToString(),
                                        reader["governorate"].ToString());
                donor.id = int.Parse(reader["id"].ToString());
                donor.addressLine2 = reader["address_line2"].ToString();
                Donors.Add(donor);
            }
            reader.Close();
            return Donors;
        }

        public Donor findByID(string id)
        {
            try
            {
            command.CommandText = "select * from donor where id=:donor_id";
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("donor_id", id);

            OracleDataReader reader = command.ExecuteReader();
            Donor foundDonor;
            if (reader.Read())
            {
                foundDonor = new Donor(
                                        reader["name"].ToString(),
                                        reader["mobile"].ToString(),
                                        reader["address_line1"].ToString(),
                                        reader["city"].ToString(),
                                        reader["governorate"].ToString());
                foundDonor.id = int.Parse(reader["id"].ToString());
                foundDonor.addressLine2 = reader["address_line2"].ToString();
            }
            else
            {
                reader.Close();
                    return null;
            }
            return foundDonor;
     
        }
             catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
        //insert
        public Donor save(Donor model)
        {
            model.id = getMaxID() + 1;
            command.CommandText = "insert into donor values (:id, :name, :mobile, :address_line1, :address_line2, :city, :governorate)";
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("id", model.id);
            command.Parameters.Add("name", model.name);
            command.Parameters.Add("mobile", model.mobile);
            command.Parameters.Add("address_line1", model.addressLine1);
            command.Parameters.Add("address_line2", model.addressLine2);
            command.Parameters.Add("city", model.city);
            command.Parameters.Add("governorate", model.governorate);
            
            int check = command.ExecuteNonQuery();
            if (check != -1)
            {
                return model;
            }
            return null;
        }

        public bool update(Donor newModel)
        {
            command.CommandText = "update donor set name =:name, mobile =:mobile, address_line1 =:address_line1, address_line2 =:address_line2, city =:city, governorate =:governorate where id =: donor_id";
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("name", newModel.name);
            command.Parameters.Add("mobile", newModel.mobile);
            command.Parameters.Add("address_line1", newModel.addressLine1);
            command.Parameters.Add("address_line2", newModel.addressLine2);
            command.Parameters.Add("city", newModel.city);
            command.Parameters.Add("governorate", newModel.governorate);
            command.Parameters.Add("donor_id", newModel.id);

            int check = command.ExecuteNonQuery();
            if(check == -1)
            {
                return false;
            }
            return true;

        }

        public int getMaxID()
        {
            command.CommandText = "select max(id) from donor";
            command.CommandType = System.Data.CommandType.Text;
            try
            {
            OracleDataReader reader = command.ExecuteReader();
            Console.WriteLine(reader);
            if (reader.Read())
            {
                return int.Parse(reader["max(id)"].ToString());
            }
            else
            {
                reader.Close();
            }
            reader.Close();

            return 0;
            }
            catch(Exception e)
            {
                return 0;
            }
        }
    }
}
