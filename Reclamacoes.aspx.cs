using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SW
{
    public partial class _Default : Page
    {
        string id_C;

        string connectionString = "Data Source=JARVIS;Initial Catalog=SW;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            CarregarGridView();

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
                     Gender, PhoneNumber, EmailConfidencial, RedressDate, ProposedDecision, Outcome, 
                     FinalDecision, Authority, RemedialDate, ActualRemedialDate, PersonVerifying, 
                    CloseOut, Escalation, EntityReferred, Contact, DateMentioned, RemedialAction, CloseDate,Status
                    ) VALUES (
                     @DateComplaint, @PAPid, @NameOfComplainant, @ComplainantAdress, @Comunity, @Distrit,@ProjectRelated, 
                    @Method, @InternalPerson, 
                    @Description,@TypeofComplaint ,
                    @Gender, @PhoneNumber, @EmailConfidencial, @RedressDate, 
                    @ProposedDecision, @Outcome, @FinalDecision, @Authority, @RemedialDate, 
                    @ActualRemedialDate, @PersonVerifying, @CloseOut, @Escalation, @EntityReferred, @Contact, 
                    @DateMentioned, @RemedialAction, @CloseDate,'Registada')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                        //command.Parameters.AddWithValue("@ComplaintNr", string.IsNullOrEmpty(complaintNr.Value) ? DBNull.Value : (object)complaintNr.Value);
                        command.Parameters.AddWithValue("@DateComplaint", string.IsNullOrEmpty(dateComplaint.Value) ? DBNull.Value : (object)dateComplaint.Value);
                        command.Parameters.AddWithValue("@PAPid", string.IsNullOrEmpty(PAPid.Value) ? DBNull.Value : (object)PAPid.Value);
                        
                        
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



                        command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDate.Value) ? DBNull.Value : (object)redressDate.Value);
                        command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecision.Value) ? DBNull.Value : (object)proposedDecision.Value);
                        command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcome.Value) ? DBNull.Value : (object)outcome.Value);
                        //command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                        command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecision.Value) ? DBNull.Value : (object)finalDecision.Value);
                        command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authority.Value) ? DBNull.Value : (object)authority.Value);
                        command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDate.Value) ? DBNull.Value : (object)remedialDate.Value);
                        command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDate.Value) ? DBNull.Value : (object)actualremedialDate.Value);
                        command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifying.Value) ? DBNull.Value : (object)personVerifying.Value);
                        command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUt.Value) ? DBNull.Value : (object)closeOUt.Value);
                        
                        
                        command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalation.Value) ? DBNull.Value : (object)escalation.Value);
                        command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferred.Value) ? DBNull.Value : (object)entityReferred.Value);
                        command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contact.Value) ? DBNull.Value : (object)contact.Value);
                        command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentioned.Value) ? DBNull.Value : (object)dateMentioned.Value);
                        command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialAction.Value) ? DBNull.Value : (object)remedialAction.Value);
                        command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDate.Value) ? DBNull.Value : (object)closeDate.Value);

                        // Abrir conexão e executar o comando
                        connection.Open();
                        command.ExecuteNonQuery();
                        CarregarGridView();
                    }


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
                    complaintNrE.Value = id_C;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT * FROM Queixas where ComplaintNr='" + id_C + "'");
                        con.Open();
                        cmd.Connection = con;

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            nameOfComplainantE.Value = sdr["NameOfComplainant"].ToString();

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
                    complaintNrE.Value = id_C;






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


        protected void upload_Click(object sender, EventArgs e) { }
        
        public void upload()
        {
            if (true)
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
                                            string query = "update Queixas set Type=@ContentType ,FileIO=@Data where ComplaintNr="+complaintNrE.Value+"";
                                            using (SqlCommand cmd = new SqlCommand(query))
                                            {
                                                cmd.Connection = con;
                                                cmd.Parameters.AddWithValue("@ContentType", contentType);
                                                cmd.Parameters.AddWithValue("@Data", bytes);
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
       
        }

        protected void exitModal_Click(object sender, EventArgs e)
        {
            string script = "  $(document).ready(function() { $('#editarQueixa').modal('hide'); });";

            ClientScript.RegisterStartupScript(this.GetType(), "OpenModal", script, true);

        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            update_All();

        }

        public void update_All()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Criação da query SQL com parâmetros
                string query = @"
                    Update Queixas set 
                     DateComplaint=@DateComplaint, PAPid=@PAPid,
                     NameOfComplainant=@NameOfComplainant, ComplainantAdress=@ComplainantAdress,
                     Comunity=@Comunity, Distrit=@Distrit, 
                     ProjectRelated=@ProjectRelated, Method=@Method , 
                     InternalPerson=@InternalPerson, Description=@Description,TypeofComplaint=@TypeofComplaint, 
                     Gender=@Gender, PhoneNumber=@PhoneNumber, EmailConfidencial= @EmailConfidencial,
                     RedressDate=@RedressDate, ProposedDecision=@ProposedDecision, Outcome= @Outcome, 
                     FinalDecision=@FinalDecision, Authority=@Authority, RemedialDate=@RemedialDate, 
                     ActualRemedialDate=@ActualRemedialDate, PersonVerifying= @PersonVerifying, 
                    CloseOut=@CloseOut, Escalation=@Escalation, EntityReferred=@EntityReferred, 
                    Contact=@Contact, DateMentioned= @DateMentioned, RemedialAction=@RemedialAction, CloseDate=@CloseDate,Status='Nivel_1'
                     where ComplaintNr=4";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adiciona os parâmetros utilizando diretamente os IDs dos campos
                    command.Parameters.AddWithValue("@ComplaintNr", string.IsNullOrEmpty(complaintNr.Value) ? DBNull.Value : (object)complaintNr.Value);
                    command.Parameters.AddWithValue("@DateComplaint", string.IsNullOrEmpty(dateComplaint.Value) ? DBNull.Value : (object)dateComplaint.Value);
                    command.Parameters.AddWithValue("@PAPid", string.IsNullOrEmpty(PAPid.Value) ? DBNull.Value : (object)PAPid.Value);


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



                    command.Parameters.AddWithValue("@RedressDate", string.IsNullOrEmpty(redressDate.Value) ? DBNull.Value : (object)redressDate.Value);
                    command.Parameters.AddWithValue("@ProposedDecision", string.IsNullOrEmpty(proposedDecision.Value) ? DBNull.Value : (object)proposedDecision.Value);
                    command.Parameters.AddWithValue("@Outcome", string.IsNullOrEmpty(outcome.Value) ? DBNull.Value : (object)outcome.Value);
                    //command.Parameters.AddWithValue("@ComplainantName", string.IsNullOrEmpty(complainantName.Value) ? DBNull.Value : (object)complainantName.Value);
                    command.Parameters.AddWithValue("@FinalDecision", string.IsNullOrEmpty(finalDecision.Value) ? DBNull.Value : (object)finalDecision.Value);
                    command.Parameters.AddWithValue("@Authority", string.IsNullOrEmpty(authority.Value) ? DBNull.Value : (object)authority.Value);
                    command.Parameters.AddWithValue("@RemedialDate", string.IsNullOrEmpty(remedialDate.Value) ? DBNull.Value : (object)remedialDate.Value);
                    command.Parameters.AddWithValue("@ActualRemedialDate", string.IsNullOrEmpty(actualremedialDate.Value) ? DBNull.Value : (object)actualremedialDate.Value);
                    command.Parameters.AddWithValue("@PersonVerifying", string.IsNullOrEmpty(personVerifying.Value) ? DBNull.Value : (object)personVerifying.Value);
                    command.Parameters.AddWithValue("@CloseOut", string.IsNullOrEmpty(closeOUt.Value) ? DBNull.Value : (object)closeOUt.Value);


                    command.Parameters.AddWithValue("@Escalation", string.IsNullOrEmpty(escalation.Value) ? DBNull.Value : (object)escalation.Value);
                    command.Parameters.AddWithValue("@EntityReferred", string.IsNullOrEmpty(entityReferred.Value) ? DBNull.Value : (object)entityReferred.Value);
                    command.Parameters.AddWithValue("@Contact", string.IsNullOrEmpty(contact.Value) ? DBNull.Value : (object)contact.Value);
                    command.Parameters.AddWithValue("@DateMentioned", string.IsNullOrEmpty(dateMentioned.Value) ? DBNull.Value : (object)dateMentioned.Value);
                    command.Parameters.AddWithValue("@RemedialAction", string.IsNullOrEmpty(remedialAction.Value) ? DBNull.Value : (object)remedialAction.Value);
                    command.Parameters.AddWithValue("@CloseDate", string.IsNullOrEmpty(closeDate.Value) ? DBNull.Value : (object)closeDate.Value);
                    upload();
                    // Abrir conexão e executar o comando
                    connection.Open();
                    command.ExecuteNonQuery();
                    CarregarGridView();
                }


            }


        }
    }
}