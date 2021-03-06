﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Yahtzee
{
  public partial class Yahtzee : Form
  {
    int aantalTeerlingen = 5;
    int maxAantalWorpen = 3;
    int aantalWorpen;
    List<TeerlingController> teerlingen = new List<TeerlingController>();
    //ScoreController scoreController = new ScoreController();
    TurnController turnController = new TurnController();
    MPScoreController mpScoreController = new MPScoreController();
    string throwsText = "Throws: ";

    public Yahtzee()
    {
      InitializeComponent();
    }

    private void Yahtzee_Load(object sender, EventArgs e)
    {
      for (int i = 0; i < aantalTeerlingen; i++)
      {
        TeerlingController tijdelijkeTeerling = new TeerlingController();
        teerlingen.Add(tijdelijkeTeerling);

        TeerlingView tView = teerlingen[i].GetView();

        int horizontalPosition = i * tView.Width;
        tView.Location = new System.Drawing.Point(horizontalPosition, 0);
        Controls.Add(tView);

        ThrowsLabel.Text = throwsText + aantalWorpen + "/" + maxAantalWorpen;
      }

      //ScoreView sView = scoreController.GetView();

      //int scoreHorizontalPosition = aantalTeerlingen * sView.Width;
      //sView.Location = new System.Drawing.Point(scoreHorizontalPosition, 0);
      //Controls.Add(sView);

      TurnView turnView = turnController.GetView();

      int turnVerticalPosition = turnView.Height + 100;
      turnView.Location = new Point(1, turnVerticalPosition);
      Controls.Add(turnView);

      MPScoreView mpScoreView = mpScoreController.GetView();
      int mpScoreHorizontalPosition = turnView.Width;
      mpScoreView.Location = new Point(mpScoreHorizontalPosition, turnVerticalPosition - 6);
      Controls.Add(mpScoreView);
    }

    private void AlgemeneWerpBtn_Click(object sender, EventArgs e)
    {
      if ( aantalWorpen < maxAantalWorpen )
      {
        for (int i = 0; i < aantalTeerlingen; i++)
        {
          teerlingen[i].Werp();
          teerlingen[i].UpdateUI();
          //scoreController.scoreModel.CurrentScore += teerlingen[i].teerlingModel.AantalOgen;
        }
        aantalWorpen++;
      }
      ThrowsLabel.Text = throwsText + aantalWorpen + "/" + maxAantalWorpen;
      //scoreController.UpdateScore();
      //scoreController.UpdateUI();
      //scoreController.scoreModel.CurrentScore = 0;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      if (checkBox1.Checked)
      {
        for (int i = 0; i < aantalTeerlingen; i++)
        {
          teerlingen[i].ShowWerpBtn();
        }
      }
      else
      {
        for (int i = 0; i < aantalTeerlingen; i++)
        {
          teerlingen[i].HideWerpBtn();
        }
      }
    }

    private void RetryBtn_Click(object sender, EventArgs e)
    {
      turnController.UpdateTurn();
      turnController.UpdateUI();
      aantalWorpen = 0;
      //scoreController.scoreModel.CurrentScore = 0;
      ThrowsLabel.Text = throwsText + aantalWorpen + "/" + maxAantalWorpen;
      for (int i = 0; i < aantalTeerlingen; i++)
      {
        teerlingen[i].ClearImage();
        teerlingen[i].ResetHold();
      }
    }
  }
}
