using BAL;
using Entities;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnicalTestCasino
{
    public partial class _Default : Page
    {
        private BalPlayer businessPlayer = new BalPlayer();

        /// <summary>
        /// method that is raised when the applicaction start
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                validateGrid();
            }

        }

        /// <summary>
        /// method who get the information of the player to call the bal and try to record the information
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void btnAddPlayer_Click(object sender, EventArgs e)
        {
            setTitles();
            if (!string.IsNullOrEmpty(txtBoxFaName.Text) && !string.IsNullOrEmpty(txtBoxFName.Text) && !string.IsNullOrEmpty(txtBoxAge.Text))
            {
                Player player = new Player();
                player.FirstName = txtBoxFName.Text;
                player.MiddleName = txtBoxFaName.Text;
                player.LastName = txtBoxMName.Text;
                player.Age = Convert.ToInt32(txtBoxAge.Text);
                businessPlayer.InsertPlayer(player);
                FillGrid();
                gridDiv.Visible = true;
                MessageEmpty.Visible = false;
                ClearControlls();
            }
            else 
            {
                lblMessageAlert.Text = "Its neccessary to enter mandatory information about player \n it's indicate with an *";
                lblName.Text += "*";
                lblFName.Text += "*";
                lblAge.Text += "*";
                upAlert.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MyAlertModal", "$(function (){$('#MyAlertModal').modal();}); ", true);
            }
        }

        /// <summary>
        /// method who get the information of the player from the webform to call the bal and update the record in the database
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Player player = new Player();
            setTitlesModal();
            if (!string.IsNullOrEmpty(txtEditName.Text) && !string.IsNullOrEmpty(txtEditFName.Text) && !string.IsNullOrEmpty(txtEditAge.Text))
            {
                player.PlayerId = Convert.ToInt32(hdnId.Value);
                player.FirstName = txtEditName.Text;
                player.MiddleName = txtEditFName.Text;
                player.LastName = txtEditMName.Text;
                player.Age = Convert.ToInt32(txtEditAge.Text);

                var result = businessPlayer.UpdatePlayer(player);

                if (true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPlayerModal", "$(function (){$('#EditPlayerModal').modal('hide');;}); ", true);
                    upGvPlayers.Update();
                    ClearControllsEdit();
                    FillGrid();
                }
            }
            else
            {
                lblErrorMessage.Text += "\nIts neccessary to enter mandatory information about player \n it's indicate with an *";
                lblEditName.Text += "*";
                lblEditFName.Text += "*";
                lblEditAge.Text += "*";
                upModal.Update();
            }
            Response.Redirect(Request.RawUrl.ToString(), false);
        }
        /// <summary>
        /// method to close the modal
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPlayerModal", "$(function (){$('#EditPlayerModal').modal('hide');;}); ", true);
            Response.Redirect(Request.RawUrl.ToString(), false);
        }

        /// <summary>
        /// method who get the id from the gridview and call the bal to delete to record the information
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void gvPlayers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int UserID = Convert.ToInt32(gvPlayers.DataKeys[e.RowIndex].Value.ToString());
            BalPlayer balPlayer = new BalPlayer();
            balPlayer.DeletePlayer(UserID);
            Response.Redirect(Request.RawUrl.ToString(), false);
        }

        /// <summary>
        /// method who get the Id from the gridview and call the bal to get specifict player to fill the controllers in the modal
        /// </summary>
        /// <param name="sender">parameter called Sender that contains a reference to the control/object that raised the event</param>
        /// <param name="e">parameter called e that contains the event data</param>
        protected void gvPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            setTitlesModal();
            var id = Convert.ToInt32(gvPlayers.DataKeys[gvPlayers.SelectedRow.RowIndex].Value.ToString());
            var player = businessPlayer.GetAPlayer(id);
            hdnId.Value = id.ToString();
            txtEditName.Text = player.FirstName;
            txtEditFName.Text = player.MiddleName;
            txtEditMName.Text = player.LastName;
            txtEditAge.Text = player.Age.ToString();
            upModal.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditPlayerModal", "$(function (){$('#EditPlayerModal').modal();}); ", true);
        }

        #region others methods to clear and fill information
        /// <summary>
        /// method to clean controlls to record a player
        /// </summary>
        public void ClearControlls()
        {
            txtBoxFName.Text = string.Empty;
            txtBoxFaName.Text = string.Empty;
            txtBoxMName.Text = string.Empty;
            txtBoxAge.Text = string.Empty;
        }

        /// <summary>
        /// method to clean controlls from the modal to edit a player
        /// </summary>
        public void ClearControllsEdit() 
        {
            hdnId.Value = string.Empty;
            txtEditName.Text = string.Empty;
            txtEditFName.Text = string.Empty;
            txtEditMName.Text = string.Empty;
            txtEditAge.Text = string.Empty;
            lblErrorMessage.Text = string.Empty;
        }

        /// <summary>
        /// method to set the titles of the labels in the webform
        /// </summary>
        public void setTitles()
        {
            lblName.Text = "First name:";
            lblFName.Text = "Father name:";
            lblAge.Text = "Age";
        }

        /// <summary>
        /// method to set the titles of the labels in the Modal when is opened
        /// </summary>
        public void setTitlesModal()
        {
            lblErrorMessage.Text = string.Empty;
            lblEditName.Text = "First name:";
            lblEditFName.Text = "Father name:";
            lblEditAge.Text = "Age";
        }

        /// <summary>
        /// method to set the list of players into the grid
        /// </summary>
        protected void FillGrid()
        {
            var list = businessPlayer.GetAllPlayers();

            gvPlayers.DataSource = list;
            gvPlayers.DataBind();
        }

        /// <summary>
        /// method to show message if grid is empty
        /// </summary>
        public void validateGrid() 
        {
            FillGrid();
            if (gvPlayers.Rows.Count == 0)
            {
                gridDiv.Visible = false;
                MessageEmpty.Visible = true;
            }
            else
            {
                gridDiv.Visible = true;
                MessageEmpty.Visible = false;
            }
        }
        #endregion
    }
}