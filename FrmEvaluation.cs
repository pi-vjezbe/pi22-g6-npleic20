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

			var evaluation = EvaluationRepository.GetEvaluation(student, currentActivity);
			if(evaluation != null)
            {
				txtTeacher.Text = evaluation.Evaluator.ToString();
				txtEvaluationDate.Text = evaluation.EvaluationDate.ToString();
				numPoints.Value = evaluation.Points;
            }
            else
            {
				txtTeacher.Text = FrmLogin.LoggedTeacher.ToString();
				txtEvaluationDate.Text = "-";	
				numPoints.Value = 0;
            }
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

        private void btnSave_Click(object sender, EventArgs e)
        {
			var teacher = FrmLogin.LoggedTeacher;
			Activity currentActivity = cboActivities.SelectedItem as Activity;
			int points = (int)numPoints.Value;
			teacher.PerformEvaluation(student, currentActivity, points);
        }
    }
}
