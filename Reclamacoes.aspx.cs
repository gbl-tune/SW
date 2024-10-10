using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class Reclamacoes : System.Web.UI.Page
    {
        string id_C;

        string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserCategory"] == null || Session["UserCategory"].ToString() == "DATABASEMANAGEROFFICER" || Session["UserCategory"].ToString() == "USERPAP")
                {
                    Response.Redirect("~/Login.aspx");
                }
                CarregarGridView();
                carregarPAP();
            }
        }

        private void CarregarGridView()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Queixas ", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int novoQueixaId;
            string novoQueixaCode = string.Empty;

            if (Page.IsValid)
            {
                // Definir a conexão com o banco de dados (você deve ajustar a string de conexão conforme seu ambiente)

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                        // Criação da query SQL com parâmetros
                        string query = @"
                    INSERT INTO Queixas (
                     DateComplaint, PAPid, NameOfComplainant, ComplainantAdress, Comunity, Distrit, 
                    ProjectRelated, Method  , InternalPerson, Description,TypeofComplaint, 
                     Gender, PhoneNumber, EmailConfidencial, Status
                    ) OUTPUT INSERTED.ComplaintNr VALUES (
                     @DateComplaint, @PAPid, @NameOfComplainant, @ComplainantAdress, @Comunity, @Distrit,@ProjectRelated, 
                    @Method, @InternalPerson, 
                    @Description,@TypeofComplaint ,
                    @Gender, @PhoneNumber, @EmailConfidencial, 'Registada')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                        //command.Parameters.AddWithValue("@ComplaintNr", string.IsNullOrEmpty(complaintNr.Value) ? DBNull.Value : (object)complaintNr.Value);
                        command.Parameters.AddWithValue("@DateComplaint", string.IsNullOrEmpty(dateComplaint.Value) ? DBNull.Value : (object)dateComplaint.Value);
                        command.Parameters.AddWithValue("@PAPid", string.IsNullOrEmpty(ddlSearch.SelectedValue) ? DBNull.Value : (object)ddlSearch.SelectedValue);


                        command.Parameters.AddWithValue("@NameOfComplainant", string.IsNullOrEmpty(nameOfComplainant.Value) ? DBNull.Value : (object)nameOfComplainant.Value);
                        command.Parameters.AddWithValue("@Confidentiality", string.IsNullOrEmpty(confidentiality.Value) ? DBNull.Value : (object)confidentiality.Value);
                        command.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender.Value) ? DBNull.Value : (object)gender.Value);
                        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber.Value) ? DBNull.Value : (object)phoneNumber.Value);
                        command.Parameters.AddWithValue("@EmailConfidencial", string.IsNullOrEmpty(emailConfidencial.Value) ? DBNull.Value : (object)emailConfidencial.Value);
                        command.Parameters.AddWithValue("@ComplainantAdress", string.IsNullOrEmpty(complainantAdress.Value) ? DBNull.Value : (object)complainantAdress.Value);
                        command.Parameters.AddWithValue("@Comunity", string.IsNullOrEmpty(comunity.Value) ? DBNull.Value : (object)comunity.Value);
                        command.Parameters.AddWithValue("@Distrit", string.IsNullOrEmpty(distrit.Value) ? DBNull.Value : (object)distrit.Value);


                        command.Parameters.AddWithValue("@ProjectRelated", string.IsNullOrEmpty(projectRelated.Value) ? DBNull.Value : (object)projectRelated.Value);
                        command.Parameters.AddWithValue("@Method", outro.Checked ? (object)otherMethod.Value : reportingMethod.Value);

                        // Outros campos
                        command.Parameters.AddWithValue("@InternalPerson", string.IsNullOrEmpty(internalPerson.Value) ? DBNull.Value : (object)internalPerson.Value);
                        command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description.Value) ? DBNull.Value : (object)description.Value);


                        command.Parameters.AddWithValue("@TypeofComplaint", outrotipo.Checked ? (object)outraClassificacao.Value : typeOfComplaint.Value);



                        //command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDate.Value) ? DBNull.Value : (object)redressDate.Value);
                        //command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecision.Value) ? DBNull.Value : (object)proposedDecision.Value);
                        //command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcome.Value) ? DBNull.Value : (object)outcome.Value);
                        ////command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                        //command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecision.Value) ? DBNull.Value : (object)finalDecision.Value);
                        //command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authority.Value) ? DBNull.Value : (object)authority.Value);
                        //command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDate.Value) ? DBNull.Value : (object)remedialDate.Value);
                        //command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDate.Value) ? DBNull.Value : (object)actualremedialDate.Value);
                        //command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifying.Value) ? DBNull.Value : (object)personVerifying.Value);
                        //command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUt.Value) ? DBNull.Value : (object)closeOUt.Value);
                        
                        
                        //command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalation.Value) ? DBNull.Value : (object)escalation.Value);
                        //command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferred.Value) ? DBNull.Value : (object)entityReferred.Value);
                        //command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contact.Value) ? DBNull.Value : (object)contact.Value);
                        //command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentioned.Value) ? DBNull.Value : (object)dateMentioned.Value);
                        //command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialAction.Value) ? DBNull.Value : (object)remedialAction.Value);
                        //command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDate.Value) ? DBNull.Value : (object)closeDate.Value);


                        connection.Open();
                        novoQueixaId = (int)command.ExecuteScalar(); // Retorna o QueixaId gerado\
                    }

                        // Gera o código alfanumérico com base no ID gerado (ex.: "Q001", "Q002")
                        novoQueixaCode = $"Q{novoQueixaId:0000}";
                        string queryAtualizarQueixa = "UPDATE Queixas SET ComplaintCode = @QueixaCode WHERE ComplaintNr = @QueixaId";

                        using (SqlCommand cmdAtualizar = new SqlCommand(queryAtualizarQueixa, connection))
                        {
                            cmdAtualizar.Parameters.AddWithValue("@QueixaCode", novoQueixaCode);
                            cmdAtualizar.Parameters.AddWithValue("@QueixaId", novoQueixaId);
                            cmdAtualizar.ExecuteNonQuery(); // Executa a atualização
                        }

                        // Abrir conexão e executar o comando
                        //command.ExecuteNonQuery();
                        CarregarGridView();


                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                //Verifica que botao foi pressionado comparando o CommnadName
                if (e.CommandName == "Editar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    id_C = row.Cells[1].Text;
                    //complaintNrE.Value = id_C;
                    

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Queixas where ComplaintNr='" + id_C + "'");
                        con.Open();
                        cmd.Connection = con;
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            tipoE.Value = !string.IsNullOrEmpty(sdr["C/S"].ToString())? sdr["C/S"].ToString():"";///////
                            complaintNrE.Value = sdr["ComplaintCode"].ToString().Trim();
                            DateTime dateValueI = Convert.ToDateTime(sdr["DateComplaint"]);
                            dateComplaintE.Value = dateValueI.ToString("yyyy-MM-dd");

                            confidentialityE.Value = sdr["Confidentiality"].ToString();//////
                            nameOfComplainantE.Value = sdr["NameOfComplainant"].ToString();
                            genderE.Value = sdr["Gender"].ToString();
                            phoneNumberE.Value = sdr["PhoneNumber"].ToString();
                            emailConfidencialE.Value = sdr["EmailConfidencial"].ToString();
                            complainantAdressE.Value = sdr["ComplainantAdress"].ToString();
                            comunityE.Value = sdr["Comunity"].ToString();
                            distritE.Value = sdr["Distrit"].ToString();
                            projectRelatedE.Value = sdr["ProjectRelated"].ToString();
                            reportingMethodE.Value = !string.IsNullOrEmpty(sdr["Method"].ToString())? sdr["Method"].ToString():"";
                            //other

                            internalPersonE.Value = sdr["InternalPerson"].ToString();
                            descriptionE.Value = sdr["Description"].ToString();

                            typeOfComplaintE.Value = !string.IsNullOrEmpty(sdr["TypeofComplaint"].ToString()) ? sdr["TypeofComplaint"].ToString():" ";
                            //other


                            if (!string.IsNullOrEmpty(sdr["RedressDate"].ToString()))
                            {
                                redressDateE.Value = Convert.ToDateTime(sdr["RedressDate"]).ToString("yyyy-MM-dd");

                            }
                            proposedDecisionE.Value = !string.IsNullOrEmpty(sdr["ProposedDecision"].ToString()) ? sdr["ProposedDecision"].ToString() : "";
                            outcomeE.Value = !string.IsNullOrEmpty(sdr["Outcome"].ToString()) ? sdr["Outcome"].ToString() : "";
                            finalDecisionE.Value = !string.IsNullOrEmpty(sdr["FinalDecision"].ToString()) ? sdr["FinalDecision"].ToString() : "";
                            authorityE.Value = !string.IsNullOrEmpty(sdr["Authority"].ToString()) ? sdr["Authority"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["ActualRemedialDate"].ToString()))
                            {
                                actualremedialDateE.Value = Convert.ToDateTime(sdr["ActualRemedialDate"]).ToString("yyyy-MM-dd");

                            }
                           
                            
                            personVerifyingE.Value = !string.IsNullOrEmpty(sdr["PersonVerifying"].ToString()) ? sdr["PersonVerifying"].ToString() : "";

                            if (!string.IsNullOrEmpty(sdr["CloseOut"].ToString()))
                            {
                                closeOUtE.Value = Convert.ToDateTime(sdr["CloseOut"]).ToString("yyyy-MM-dd");

                            }
                           

                            escalationE.Value = !string.IsNullOrEmpty(sdr["Escalation"].ToString()) ? sdr["Escalation"].ToString() : "";
                            entityReferredE.Value = !string.IsNullOrEmpty(sdr["EntityReferred"].ToString()) ? sdr["EntityReferred"].ToString() : "";
                            contactE.Value = !string.IsNullOrEmpty(sdr["Contact"].ToString()) ? sdr["Contact"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["DateMentioned"].ToString()))
                            {
                                dateMentionedE.Value = Convert.ToDateTime(sdr["DateMentioned"]).ToString("yyyy-MM-dd");

                            }

                            
                            remedialActionE.Value = !string.IsNullOrEmpty(sdr["RemedialAction"].ToString()) ? sdr["RemedialAction"].ToString() : "";
                            if (!string.IsNullOrEmpty(sdr["CloseDate"].ToString()))
                            {
                                closeDateE.Value = Convert.ToDateTime(sdr["CloseDate"]).ToString("yyyy-MM-dd");

                            }

                            carregarddl(ddlEstadoSecG, "G", sdr["ComplaintNr"].ToString());

                            //DateTime dateValueI = Convert.ToDateTime(sdr["DataIngresso"]);
                            //dataIngresso.Value = dateValueI.ToString("yyyy-MM-dd");
                            //dataFim.Value = sdr["DataFiim"].ToString();
                            //departamento.Value = sdr["Departamento"].ToString();
                            //nivelAcademico.Value = sdr["NivelAcademico"].ToString();
                            //contacto.Value = sdr["Contacto"].ToString();

                        }




                        con.Close();


                    }




                    string script = "  $(document).ready(function() { $('#editarQueixa').modal('toggle'); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);

                }
                else 

                    if (e.CommandName == "Download")
                    {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    id_C = row.Cells[1].Text;
                    //complaintNrE.Value = id_C;






                    byte[] bytes;
                    string fileName, contentType;
                    
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                        //Response.Write("<script>alert('gjgj')</script>");
                            cmd.CommandText = "select  NameOfComplainant, FileIO, Type from Queixas where ComplaintNr=@Id";
                            cmd.Parameters.AddWithValue("@Id", id_C);
                            cmd.Connection = con;
                            con.Open();
                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                sdr.Read();
                                bytes = (byte[])sdr["FileIO"];
                                contentType = sdr["Type"].ToString();
                                fileName = sdr["NameOfComplainant"].ToString();
                            }
                            if (bytes == null)
                            {
                                Response.Write("<script>alert('Nao existe ficheiro')</script>");

                            }
                            con.Close();
                        }
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                   

                }
            }
            catch (Exception ev)
            {
                Response.Write("<script>alert('" + ev.Message.ToString() + "')</script>");

            }
        }


        protected void upload_Click(object sender, EventArgs e) {  }

        public void upload()
        {



            try
            {




                string filePath = FileUploadIO.PostedFile.FileName; // getting the file path of uploaded file  
                string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename1); // getting the file extension of uploaded file
                string type = String.Empty;

                if (!FileUploadIO.HasFile)
                {
                    Response.Write("Selecione Um Ficheiro");
                }
                if (FileUploadIO.HasFile)
                {
                    try
                    {
                        switch (ext) // this switch code validate the files which allow to upload only PDF file   
                        {
                            case ".pdf":
                                type = "application/pdf";
                                break;
                        }
                        if (type != String.Empty)
                        {
                            string contentType = FileUploadIO.PostedFile.ContentType;
                            using (Stream fs = FileUploadIO.PostedFile.InputStream)
                            {
                                using (BinaryReader br = new BinaryReader(fs))
                                {
                                    byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                    using (SqlConnection con = new SqlConnection(connectionString))
                                    {
                                        string query = "update Queixas set Type=@ContentType ,FileIO=@Data where ComplaintCode='" + complaintNrE.Value + "'";
                                        //string query = "update Queixas set Type=@ContentType ,FileIO=@Data where ComplaintCode='Q0003'";
                                        using (SqlCommand cmd = new SqlCommand(query))
                                        {
                                            cmd.Connection = con;
                                            cmd.Parameters.AddWithValue("@ContentType", contentType);
                                            cmd.Parameters.AddWithValue("@Data", bytes);
                                            //cmd.Parameters.AddWithValue("@id",complaintNrE.Value);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                            }
                            Response.Redirect(Request.Url.AbsoluteUri);




                        }
                        //se for foto

                        //else
                        //{
                        //    using (SqlConnection con = new SqlConnection(connectionString))
                        //    {
                        //        byte[] myphoto = FileUploadIO.FileBytes;
                        //        if (con.State == ConnectionState.Closed)
                        //            con.Open();
                        //        string insertUplod = "update tbl_Estagiario set foto=@pic where ordem=@id";
                        //        SqlCommand insertCmd = new SqlCommand(insertUplod, con);
                        //        insertCmd.Parameters.AddWithValue("@pic", myphoto);
                        //        insertCmd.Parameters.AddWithValue("@id", complaintNrE.Value);
                        //        insertCmd.ExecuteNonQuery();
                        //        con.Close();
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message.ToString());
                    }
                }

            }
            catch (Exception evt)
            {

                Response.Write("<script>alert('" + evt.Message.ToString() + "')</script>");
            }




        }

        protected void exitModal_Click(object sender, EventArgs e)
        {
            string script = "  $(document).ready(function() { $('#editarQueixa').modal('hide'); });";

            ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);

        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            string id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("select ComplaintNr from Queixas where ComplaintCode='"+complaintNrE.Value+"'", connection))
                {
                    
                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        sdr.Read();
                        id = sdr["ComplaintNr"].ToString().Trim();


                      
                    }
                }
            }
            update_All(id);

        }

        public void update_All(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                     //DateComplaint=@DateComplaint, PAPid=@PAPid,
                     //NameOfComplainant=@NameOfComplainant, ComplainantAdress=@ComplainantAdress,
                     //Comunity=@Comunity, Distrit=@Distrit, 
                     //ProjectRelated=@ProjectRelated, Method=@Method , 
                     //InternalPerson=@InternalPerson, Description=@Description,TypeofComplaint=@TypeofComplaint, 
                     //Gender=@Gender, PhoneNumber=@PhoneNumber, EmailConfidencial= @EmailConfidencial,
                // Criação da query SQL com parâmetros
                string query = @"
                    Update Queixas set 
                     RedressDate=@RedressDate, ProposedDecision=@ProposedDecision, Outcome= @Outcome, 
                     FinalDecision=@FinalDecision, Authority=@Authority, RemedialDate=@RemedialDate, 
                     ActualRemedialDate=@ActualRemedialDate, PersonVerifying= @PersonVerifying, 
                    CloseOut=@CloseOut, Escalation=@Escalation, EntityReferred=@EntityReferred, 
                    Contact=@Contact, DateMentioned= @DateMentioned, RemedialAction=@RemedialAction, CloseDate=@CloseDate,Status='Nivel_1'
                     where ComplaintCode='"+complaintNrE.Value+"'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                    //command.Parameters.AddWithValue("@ComplaintNr", string.IsNullOrEmpty(complaintNr.Value) ? DBNull.Value : (object)complaintNr.Value);

                    //command.Parameters.AddWithValue("@DateComplaint", string.IsNullOrEmpty(dateComplaint.Value) ? DBNull.Value : (object)dateComplaint.Value);
                    //command.Parameters.AddWithValue("@PAPid", string.IsNullOrEmpty(PAPid.Value) ? DBNull.Value : (object)PAPid.Value);


                    //command.Parameters.AddWithValue("@NameOfComplainant", string.IsNullOrEmpty(nameOfComplainant.Value) ? DBNull.Value : (object)nameOfComplainant.Value);
                    //command.Parameters.AddWithValue("@Confidentiality", string.IsNullOrEmpty(confidentiality.Value) ? DBNull.Value : (object)confidentiality.Value);
                    //command.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender.Value) ? DBNull.Value : (object)gender.Value);
                    //command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber.Value) ? DBNull.Value : (object)phoneNumber.Value);
                    //command.Parameters.AddWithValue("@EmailConfidencial", string.IsNullOrEmpty(emailConfidencial.Value) ? DBNull.Value : (object)emailConfidencial.Value);
                    //command.Parameters.AddWithValue("@ComplainantAdress", string.IsNullOrEmpty(complainantAdress.Value) ? DBNull.Value : (object)complainantAdress.Value);
                    //command.Parameters.AddWithValue("@Comunity", string.IsNullOrEmpty(comunity.Value) ? DBNull.Value : (object)comunity.Value);
                    //command.Parameters.AddWithValue("@Distrit", string.IsNullOrEmpty(distrit.Value) ? DBNull.Value : (object)distrit.Value);


                    //command.Parameters.AddWithValue("@ProjectRelated", string.IsNullOrEmpty(projectRelated.Value) ? DBNull.Value : (object)projectRelated.Value);
                    //command.Parameters.AddWithValue("@Method", outro.Checked ? (object)otherMethod.Value : reportingMethod.Value);

                    //// Outros campos
                    //command.Parameters.AddWithValue("@InternalPerson", string.IsNullOrEmpty(internalPerson.Value) ? DBNull.Value : (object)internalPerson.Value);
                    //command.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description.Value) ? DBNull.Value : (object)description.Value);


                    //command.Parameters.AddWithValue("@TypeofComplaint", outrotipo.Checked ? (object)outraClassificacao.Value : typeOfComplaint.Value);



                    command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDateE.Value) ? DBNull.Value : (object)redressDateE.Value);
                    command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecisionE.Value) ? DBNull.Value : (object)proposedDecisionE.Value);
                    command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcomeE.Value) ? DBNull.Value : (object)outcomeE.Value);
                    //command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                    command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecisionE.Value) ? DBNull.Value : (object)finalDecisionE.Value);
                    command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authorityE.Value) ? DBNull.Value : (object)authorityE.Value);
                    command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDateE.Value) ? DBNull.Value : (object)remedialDateE.Value);
                    command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDateE.Value) ? DBNull.Value : (object)actualremedialDateE.Value);
                    command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifyingE.Value) ? DBNull.Value : (object)personVerifyingE.Value);
                    command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUtE.Value) ? DBNull.Value : (object)closeOUtE.Value);


                    command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalationE.Value) ? DBNull.Value : (object)escalationE.Value);
                    command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferredE.Value) ? DBNull.Value : (object)entityReferredE.Value);
                    command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contactE.Value) ? DBNull.Value : (object)contactE.Value);
                    command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentionedE.Value) ? DBNull.Value : (object)dateMentionedE.Value);
                    command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialActionE.Value) ? DBNull.Value : (object)remedialActionE.Value);
                    command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDateE.Value) ? DBNull.Value : (object)closeDateE.Value);

                    //faz upload do documento
                    connection.Open();
                    upload();

                    // Salvar o histórico da transição
                    SqlCommand cmdHistorico = new SqlCommand("INSERT INTO HistoricoEstados (QueixaId, Estadoid,DataTransicao,Username) VALUES (@QueixaId, @Estado,@DataMudanca,@Username)", connection);
                    cmdHistorico.Parameters.AddWithValue("@QueixaId", id);
                    cmdHistorico.Parameters.AddWithValue("@Estado", ddlEstadoSecG.SelectedValue);
                    cmdHistorico.Parameters.AddWithValue("@DataMudanca", DateTime.Now);
                    cmdHistorico.Parameters.AddWithValue("@Username", Session["User"]);
                    cmdHistorico.ExecuteNonQuery();

                    // Abrir conexão e executar o comando
                    command.ExecuteNonQuery();
                    CarregarGridView();
                }


            }


        }

        public void datas()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  NameOfComplainant, FileIO, Type from Queixas where ComplaintNr=@Id";
                    cmd.Parameters.AddWithValue("@Id", id_C);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read()) { 
                        DateTime DateComplaint = sdr.GetDateTime(1);
                        string status = sdr["Status"].ToString();
                        string id = sdr["ComplaintNr"].ToString();
                            if(DateTime.Now >= DateComplaint.AddDays(10) && status=="Nivel1" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel2" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel3" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(15) && status=="Nivel4" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel5" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            } if(DateTime.Now >= DateComplaint.AddDays(5) && status=="Nivel6" ) {
                                //mudar o status para atrasada X dias e dizer o vivel em que esta
                                //ou criar outra coluna 

                                //faz calculo da diferenca de dias
                                compara(con, id,status,DateComplaint);


                            }
                        }
                    }

                    con.Close();
                }
            }




        }
        public void compara(SqlConnection con, string id, string status, DateTime DateComplaint)
        {
            //faz calculo da diferenca de dias
            DateTime dataAtual = DateTime.Now;
            TimeSpan diferenca = dataAtual - DateComplaint;
            int diasPassados = diferenca.Days;
            string msg = ("Past " + diasPassados + "days from " + status);
            string query = "update Queixas set Atraso=@Atraso where ComplaintNr=" + id + "";
            using (SqlCommand cmdI = new SqlCommand(query))
            {
                cmdI.Connection = con;
                cmdI.Parameters.AddWithValue("@Atraso", msg);

                con.Open();
                cmdI.ExecuteNonQuery();
                con.Close();
            }
        }


        //carregar as dropdown dos estados
        public void carregarddl(DropDownList dll,string Sec,string Id)
        {
            string query = "SELECT  EstadoId, Estado FROM Estados WHERE Sec='"+ Sec +"' and EstadoId NOT IN ( SELECT EstadoId  FROM HistoricoEstados WHERE QueixaId ='" +Id +"')";
            DataTable Estado = Database.ExecuteQuery(query);

            dll.DataSource = Estado;
            dll.DataTextField = "Estado";
            dll.DataValueField = "EstadoId";
            dll.DataBind();
        }


        //carregar lista de PAPs no dropdown
        public void carregarPAP()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * from PAP";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                // Executar a query e ler os resultados
                SqlDataReader dr = cmd.ExecuteReader();
                ddlSearch.DataSource = dr;
                ddlSearch.DataTextField = "Nome";
                ddlSearch.DataValueField = "PAPid";
                ddlSearch.DataBind();

                // Adiciona um item inicial de seleção
                ddlSearch.Items.Insert(0, new ListItem("PAP Name", "0"));
            }




        }

        //qunado e selecionado um PAP deve buscar dados da base de dados e popular os camos da SEC B
        protected void populate(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("select * from PAP where PAPid='" + ddlSearch.SelectedValue.ToString() + "'", connection))
                {

                    connection.Open();
                    using (SqlDataReader sdr = command.ExecuteReader())
                    {
                        sdr.Read();
                        nameOfComplainant.Value = sdr["Nome"].ToString().Trim();
                        gender.Value = sdr["Gender"].ToString().Trim();
                        phoneNumber.Value = sdr["Phone"].ToString().Trim();
                        emailConfidencial.Value = sdr["Email"].ToString().Trim();
                        complainantAdress.Value = sdr["Adress"].ToString().Trim();
                        comunity.Value = sdr["Community"].ToString().Trim();
                        distrit.Value = sdr["District"].ToString().Trim();



                    }
                }
            }
            string script = "  $(document).ready(function() { $('#novaQueixa').modal('toggle'); });";
            ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);
        }
        
    }    
}