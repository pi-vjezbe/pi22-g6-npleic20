using Evaluation_Manager.Models;
using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_Manager
{
	public partial class FrmEvaluation : Form
	{
		private Student student;
		public FrmEvaluation(Student selectedStudent)
		{
			InitializeComponent();
			student = selectedStudent;
		}

		private void FrmEvaluation_Load(object sender, EventArgs e)
		{
			Text = student.FirstName + " " + student.LastName;
			List<Activity> activities = ActivityRepository.GetActivities();
			cboActivities.DataSource = activities;
		}

		private void cboActivities_SelectedIndexChanged(object sender, EventArgs e)
		{
			Activity currentActivity = cboActivities.SelectedItem as Activity;
			txtActivityDescription.Text = currentActivity.Description;
			txtMinForGrade.Text = currentActivity.MinPointsForGrade + "/" + currentActivity.MaxPionts;
			txtMinForSignature.Text = currentActivity.MinPointsForSignature + "/" + currentActivity.MaxPionts;
			numPoints.Minimum = 0;
			numPoints.Maximum = currentActivity.MaxPionts;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
