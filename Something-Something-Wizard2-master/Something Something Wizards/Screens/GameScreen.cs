﻿//Nicholas Levesque June 14, 2019 - This program is designed to demonstrate all that I have learned this year in computer science and is supposed to be a fun game
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
namespace Something_Something_Wizards
{
    public partial class GameScreen : UserControl
    {   //Bool variables for various uses
        public static bool fun, randomlyGeneratedAttack, aiTurn, manaNoCheat, keyPress, attackPhase, possibleAttacks,
        attackInMotion, passPhase, insufficientMana;
        //Sound Players
       public static SoundPlayer player = new SoundPlayer(Properties.Resources.song);
        SoundPlayer explosionPlayer = new SoundPlayer(Properties.Resources.Atomic_Bomb);
        SoundPlayer screamPlayer = new SoundPlayer(Properties.Resources.Scary_Scream);
        SoundPlayer electrcityPlayer = new SoundPlayer(Properties.Resources.Electricity);
        //Intger variables for various uses
        int trollCounter, attackSelector, timer, aiAttackIndicator, aiSelectedCharcter;
        Random rngGenerator = new Random();
        Random aiCharcterSelector = new Random();
        int randomAiAttack;
        //Drawing Utensils
        public static Pen drawPen = new Pen(Color.Black);
        Font trollFont = new Font("Webdings", 40);
        Font trollDrawFont = new Font("Arial", 10);
        public static Font drawFont = new Font("Arial", 30);
        public static SolidBrush manaBrush = new SolidBrush(Color.Blue);
        public static SolidBrush healthBrush = new SolidBrush(Color.Red);
        public static SolidBrush drawBrush = new SolidBrush(Color.Green);
        //The Different Wizards
        Death_Wizard dk = new Death_Wizard();
        public static Death_Wizard aiDK = new Death_Wizard();
        Lightining_Wizard lk = new Lightining_Wizard();
        public static Lightining_Wizard aiLK = new Lightining_Wizard();
        MEGAMEME mega = new MEGAMEME();
        public static MEGAMEME aiMega = new MEGAMEME();

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }
        public void OnStart()
        {
            player.Stop();
            //Generates AI Charcter
            aiSelectedCharcter = aiCharcterSelector.Next(1, 4);
            //Sets AI properties
            aiDK.objectX = aiDK.aiX - 100;
            aiMega.objectX = aiMega.aiX - 100;
            aiLK.objectX = aiLK.aiX - 100;
            aiDK.name = "Steve";
            aiMega.name = "MEGAMEME";
            aiLK.name = "Harry Slaughter";
            //Sets Current Name to the wizards
            dk.name = OrignalForm.name;
            lk.name = OrignalForm.name;
            mega.name = OrignalForm.name;
            //Resets Health
            dk.health = 20;
            lk.health = 20;
            mega.health = 20;
            aiDK.health = 20;
            aiMega.health = 20;
            aiLK.health = 20;
            //Resets Mana
            dk.mana = 10;
            lk.mana = 10;
            mega.mana = 10;
            aiDK.mana = 10;
            aiMega.mana = 10;
            aiLK.mana = 10;
        }
        //Used by player to decide presented options
        public void GameScreen_KeyPress(object sender, KeyPressEventArgs e )
        {

            switch (e.KeyChar)
            {//Either begin attacks or chooses attack 1 in attack phase
                case 'z':
                    e.Handled = true;
                    keyPress = e.Handled;
                    if (e.Handled == true && possibleAttacks == false && aiTurn == false)
                    {
                        attackPhase = true;
                        e.Handled = false;
                        manaNoCheat = true;
                        fun = false;
                    }
                    if (e.Handled == true && possibleAttacks == true && aiTurn == false)
                    {
                        attackSelector = 1;
                        e.Handled = false;
                        fun = false;
                    }
                    break;
                    //Either pass or choose attack 2
                case 'c':
                    e.Handled = true;
                    keyPress = e.Handled;
                    if (e.Handled == true && dk.mana <= 10 && manaNoCheat == false && aiTurn == false
                        || lk.mana < 10 && manaNoCheat == false && aiTurn == false
                        || mega.mana < 10 && manaNoCheat == false && aiTurn == false)
                    {
                        fun = false;
                        passPhase = true;
                        e.Handled = false;
                    }
                    if (e.Handled == true && possibleAttacks == true && aiTurn == false)
                    {
                        fun = false;
                        attackSelector = 2;
                        e.Handled = false;
                    }
                    break;
                    //Chooses attack 3
                case 'v':
                    e.Handled = true;
                    keyPress = e.Handled;
                    if (e.Handled == true && possibleAttacks == true && aiTurn == false)
                    {
                        fun = false;
                        attackSelector = 3;
                        e.Handled = false;
                    }
                    else
                    {
                        fun = true;
                        trollCounter += 1;
                    }
                    break;
                    //Chooses attack 4
                case 'x':
                    e.Handled = true;
                    keyPress = e.Handled;
                    if (e.Handled == true && possibleAttacks == true && aiTurn == false)
                    {
                        fun = false;
                        attackSelector = 4;
                        e.Handled = false;
                    }
                    else
                    {
                        trollCounter += 1;
                        fun = true;
                    }
                    break;
            }
        }
        //Decides which attack was used by the player and by what charcter on what ai charcter and deals with it according;y
        private void PlayerDecidedAttacks()
        {
            switch (aiSelectedCharcter)
            {
                case 1:
                    switch (attackSelector)
                    {
                        case 1:
                            aiDK.health -= 3;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 2;
                                    break;
                                case 2:
                                    mega.mana -= 2;
                                    break;
                                case 3:
                                    lk.mana -= 2;
                                    break;
                            }
                            break;
                        case 2:
                            aiDK.health -= 6;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 5;
                                    break;
                                case 2:
                                    mega.mana -= 5;
                                    break;
                                case 3:
                                    lk.mana -= 5;
                                    break;
                            }
                            break;
                        case 3:
                            aiDK.health -= 10;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 10;
                                    break;
                                case 2:
                                    mega.mana -= 10;
                                    break;
                                case 3:
                                    lk.mana -= 10;
                                    break;
                            }
                            break;
                        case 4:
                            aiDK.health -= 3;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 3;
                                    break;
                                case 2:
                                    mega.mana -= 3;
                                    break;
                                case 3:
                                    lk.mana -= 3;
                                    break;
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (attackSelector)
                    {
                        case 1:
                            aiMega.health -= 3;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 2;
                                    break;
                                case 2:
                                    mega.mana -= 2;
                                    break;
                                case 3:
                                    lk.mana -= 2;
                                    break;
                            }
                            break;
                        case 2:
                            aiMega.health -= 6;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 5;
                                    break;
                                case 2:
                                    mega.mana -= 5;
                                    break;
                                case 3:
                                    lk.mana -= 5;
                                    break;
                            }
                            break;
                        case 3:
                            aiMega.health -= 10;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 10;
                                    break;
                                case 2:
                                    mega.mana -= 10;
                                    break;
                                case 3:
                                    lk.mana -= 10;
                                    break;
                            }
                            break;
                        case 4:
                            aiMega.health -= 3;
                            switch (OrignalForm.player_Charcter)
                            {
                                case 1:
                                    dk.mana -= 3;
                                    break;
                                case 2:
                                    mega.mana -= 3;
                                    break;
                                case 3:
                                    lk.mana -= 3;
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    {
                        switch (attackSelector)
                        {
                            case 1:
                                aiLK.health -= 3;
                                switch (OrignalForm.player_Charcter)
                                {
                                    case 1:
                                        dk.mana -= 2;
                                        break;
                                    case 2:
                                        mega.mana -= 2;
                                        break;
                                    case 3:
                                        lk.mana -= 2;
                                        break;
                                }
                                break;
                            case 2:
                                aiLK.health -= 6;
                                switch (OrignalForm.player_Charcter)
                                {
                                    case 1:
                                        dk.mana -= 5;
                                        break;
                                    case 2:
                                        mega.mana -= 5;
                                        break;
                                    case 3:
                                        lk.mana -= 5;
                                        break;
                                }
                                break;
                            case 3:
                                aiLK.health -= 10;
                                switch (OrignalForm.player_Charcter)
                                {
                                    case 1:
                                        dk.mana -= 10;
                                        break;
                                    case 2:
                                        mega.mana -= 10;
                                        break;
                                    case 3:
                                        lk.mana -= 10;
                                        break;
                                }
                                break;
                            case 4:
                                aiLK.health -= 3;
                                switch (OrignalForm.player_Charcter)
                                {
                                    case 1:
                                        dk.mana -= 3;
                                        break;
                                    case 2:
                                        mega.mana -= 3;
                                        break;
                                    case 3:
                                        lk.mana -= 3;
                                        break;
                                }
                                break;
                        }
                        break;
                    }



            }
        }
        //Same as the one above, but for the AI
        private void AiDecidedAttacks()
        {
            switch (OrignalForm.player_Charcter)
            {
                case 1:
                    switch (aiAttackIndicator)
                    {
                        case 1:
                            dk.health -= 3;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 2;
                                    break;
                                case 2:
                                    aiMega.mana -= 2;
                                    break;
                                case 3:
                                    aiLK.mana -= 2;
                                    break;
                            }
                            break;
                        case 2:
                            dk.health -= 6;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 5;
                                    break;
                                case 2:
                                    aiMega.mana -= 5;
                                    break;
                                case 3:
                                    aiLK.mana -= 5;
                                    break;
                            }
                            break;
                        case 3:
                            dk.health -= 10;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 10;
                                    break;
                                case 2:
                                    aiMega.mana -= 10;
                                    break;
                                case 3:
                                    aiLK.mana -= 10;
                                    break;
                            }
                            break;
                        case 4:
                            dk.health -= 3;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 3;
                                    break;
                                case 2:
                                    aiMega.mana -= 3;
                                    break;
                                case 3:
                                    aiLK.mana -= 3;
                                    break;
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (aiAttackIndicator)
                    {
                        case 1:
                            mega.health -= 3;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 2;
                                    break;
                                case 2:
                                    aiMega.mana -= 2;
                                    break;
                                case 3:
                                    aiLK.mana -= 2;
                                    break;
                            }
                            break;
                        case 2:
                            mega.health -= 6;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 5;
                                    break;
                                case 2:
                                    aiMega.mana -= 5;
                                    break;
                                case 3:
                                    aiLK.mana -= 5;
                                    break;
                            }
                            break;
                        case 3:
                            mega.health -= 10;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 10;
                                    break;
                                case 2:
                                    aiMega.mana -= 10;
                                    break;
                                case 3:
                                    aiLK.mana -= 10;
                                    break;
                            }
                            break;
                        case 4:
                            mega.health -= 3;
                            switch (aiSelectedCharcter)
                            {
                                case 1:
                                    aiDK.mana -= 3;
                                    break;
                                case 2:
                                    aiMega.mana -= 3;
                                    break;
                                case 3:
                                    aiLK.mana -= 3;
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    {
                        switch (aiAttackIndicator)
                        {
                            case 1:
                                lk.health -= 3;
                                switch (aiSelectedCharcter)
                                {
                                    case 1:
                                        aiDK.mana -= 2;
                                        break;
                                    case 2:
                                        aiMega.mana -= 2;
                                        break;
                                    case 3:
                                        aiLK.mana -= 2;
                                        break;
                                }
                                break;
                            case 2:
                                lk.health -= 6;
                                switch (aiSelectedCharcter)
                                {
                                    case 1:
                                        aiDK.mana -= 5;
                                        break;
                                    case 2:
                                        aiMega.mana -= 5;
                                        break;
                                    case 3:
                                        aiLK.mana -= 5;
                                        break;
                                }
                                break;
                            case 3:
                                lk.health -= 10;
                                switch (aiSelectedCharcter)
                                {
                                    case 1:
                                        aiDK.mana -= 10;
                                        break;
                                    case 2:
                                        aiMega.mana -= 10;
                                        break;
                                    case 3:
                                        aiLK.mana -= 10;
                                        break;
                                }
                                break;
                            case 4:
                                lk.health -= 3;
                                switch (aiSelectedCharcter)
                                {
                                    case 1:
                                        aiDK.mana -= 3;
                                        break;
                                    case 2:
                                        aiMega.mana -= 3;
                                        break;
                                    case 3:
                                        aiLK.mana -= 3;
                                        break;
                                }
                                break;
                        }
                        break;
                    }



            }
        }
        //Collsion Method
        public void CollsionDetail()
        {//Do on player turn
            if (aiTurn == false)
            {
                PlayerDecidedAttacks();
                attackPhase = false;
                possibleAttacks = false;
                aiTurn = true;
                switch (OrignalForm.player_Charcter)
                {
                    case 1:
                        screamPlayer.Play();
                        dk.objectX = dk.sizeX + 100;
                        break;
                    case 2:
                        explosionPlayer.Play();
                        mega.objectX = mega.sizeX + 100;
                        break;
                    case 3:
                        lk.objectX = lk.sizeX + 100;
                        electrcityPlayer.Play();
                        break;
                }
            }
            //Do on Ai turn
            else
            {
                aiTurn = false;
                randomlyGeneratedAttack = false;
                AiDecidedAttacks();
                switch (aiSelectedCharcter)
                {
                    case 1:
                        screamPlayer.Play();
                        aiDK.objectX = aiDK.aiX - 100;
                        break;
                    case 2:
                        explosionPlayer.Play();
                        aiMega.objectX = aiMega.aiX - 100;
                        break;
                    case 3:
                        electrcityPlayer.Play();
                        aiLK.objectX = aiLK.aiX - 100;
                        break;
                }
            }
            timer = 0;
            attackSelector = 0;
            aiAttackIndicator = 0;
            //Win Condtion
            if (aiMega.health <= 0 || aiDK.health <= 0 || aiLK.health <= 0)
            {
                aiTurn = false;
                Form f = this.FindForm();
                WinScreen g = new WinScreen();
                f.Controls.Remove(this);
                f.Controls.Add(g);
                g.Location = new Point((this.Width - g.Width) / 2, (this.Height - g.Height) / 2);
            }
            //Lose Condtion
            if (mega.health <= 0 || dk.health <= 0 || lk.health <= 0)
            {
                aiTurn = false;
                Form z = this.FindForm();
                LoseScreen g = new LoseScreen();
                z.Controls.Remove(this);
                z.Controls.Add(g);
                g.Location = new Point((this.Width - g.Width) / 2, (this.Height - g.Height) / 2);
                this.Dispose();
            }
        }
        private void HavingFun(PaintEventArgs e)
        {
            if (trollCounter < 10)
            {
                e.Graphics.DrawString("STOP TRYING TO CRASH THE GODDAMN PROGRAM", trollFont, drawBrush, 0, 300);
                e.Graphics.DrawString("STOP TRYING TO CRASH THE GODDAMN PROGRAM", trollDrawFont, drawBrush, 200, 400);
            }
            if (trollCounter >= 10)
            {
                e.Graphics.DrawString("YOU ASKED FOR THIS", trollFont, drawBrush, 0, 300);
                e.Graphics.DrawString("YOU ASKED FOR THIS", trollDrawFont, drawBrush, 200, 400);
                trollCounter += 1;
            }

            if (trollCounter == 15)
            {
                Thread.Sleep(6000);
                Application.Exit();
            }
        }

        //Used for when the health of the player is between 13 and 20
        private void aiAttackFirstPhase(PaintEventArgs e)
        {

            switch (aiSelectedCharcter)
            {
                case 1:
                    if (randomlyGeneratedAttack == false)
                    {
                        randomAiAttack = rngGenerator.Next(1, 5);
                        randomlyGeneratedAttack = true;
                    }
                    switch (randomAiAttack)
                    {
                        case 1:
                            if (aiDK.mana >= 2)
                            {
                                aiAttackIndicator = 1;
                                aiDK.DeathEyes(e, dk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 2:
                            if (aiDK.mana >= 5)
                            {
                                aiAttackIndicator = 2;
                                aiDK.DeathHand(e, dk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 3:
                            if (aiDK.mana >= 10)
                            {
                                aiAttackIndicator = 3;
                                aiDK.DeathSword(e, dk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 4:
                            if (aiDK.mana >= 3)
                            {
                                aiAttackIndicator = 4;
                                aiDK.Shout(e, dk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                    }
                    break;

                case 2:
                    if (randomlyGeneratedAttack == false)
                    {
                        randomAiAttack = rngGenerator.Next(1, 5);
                        randomlyGeneratedAttack = true;
                    }
                    switch (randomAiAttack)
                    {
                        case 1:
                            if (aiMega.mana >= 2)
                            {
                                aiAttackIndicator = 1;
                                aiMega.explosion(e, mega);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 2:
                            if (aiMega.mana >= 5)
                            {
                                aiAttackIndicator = 2;
                                aiMega.Explosion(e, mega);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 3:
                            if (aiMega.mana >= 10)
                            {
                                aiAttackIndicator = 3;
                                aiMega.EXPLOSION(e, mega);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 4:
                            if (aiMega.mana >= 3)
                            {
                                aiAttackIndicator = 4;
                                aiMega.Baka(e, mega);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                    }
                    break;

                case 3:
                    if (randomlyGeneratedAttack == false)
                    {
                        randomAiAttack = rngGenerator.Next(1, 5);
                        randomlyGeneratedAttack = true;
                    }
                    switch (randomAiAttack)
                    {
                        case 1:
                            if (aiLK.mana >= 2)
                            {
                                aiAttackIndicator = 1;
                                aiLK.Sparks(e, lk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 2:
                            if (aiLK.mana >= 5)
                            {
                                aiAttackIndicator = 2;
                                aiLK.Crackle(e, lk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 3:
                            if (aiLK.mana >= 10)
                            {
                                aiAttackIndicator = 3;
                                aiLK.Lighting(e, lk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                        case 4:
                            if (aiLK.mana >= 3)
                            {
                                aiAttackIndicator = 4;
                                aiLK.Lizards(e, lk);
                            }
                            else
                            {
                                notEnoughMana(e);
                            }
                            break;
                    }
                    break;
            }
        }
        //Used for player health between 12 and 10
        private void aiAttackSecondPhase(PaintEventArgs e)
        {


            switch (aiSelectedCharcter)
            {
                case 1:
                    if (aiDK.mana >= 2)
                    {
                        aiAttackIndicator = 1;
                        aiDK.DeathEyes(e, dk);
                    }
                    else
                    {
                        notEnoughMana(e);
                    }
                    break;
                case 2:
                    if (aiMega.mana >= 2)
                    {
                        aiAttackIndicator = 1;
                        aiMega.explosion(e, mega);
                    }
                    else
                    {
                        notEnoughMana(e);
                    }
                    break;
                case 3:
                    if (aiLK.mana >= 2)
                    {
                        aiAttackIndicator = 1;
                        aiLK.Sparks(e, lk);
                    }
                    else
                    {
                        notEnoughMana(e);
                    }
                    break;

            }
        }
        // Used for player health 10 or lower
        private void aiAttackPhaseThree(PaintEventArgs e)
        {

                switch (aiSelectedCharcter)
                {
                    case 1:
                        if (aiDK.mana == 10)
                        {
                            aiAttackIndicator = 3;
                            aiDK.DeathSword(e, dk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 2:
                        if (aiMega.mana == 10)
                        {
                            aiAttackIndicator = 3;
                            aiMega.EXPLOSION(e, mega);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 3:
                        if (aiLK.mana == 10)
                        {
                            aiAttackIndicator = 3;
                            aiLK.Lighting(e, lk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                }
            
        }
        private void aiPhase(PaintEventArgs e)
        {

            switch (OrignalForm.player_Charcter)
            {
                case 1:
                    if (dk.health >= 13 && dk.health <= 20)

                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2  || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackFirstPhase(e);
                        }


                    }
                    else if (dk.health <= 12 && dk.health > 10 )
                    {

                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackSecondPhase(e);
                        }
                    }
                    else if (dk.health <= 10 )
                    {
                        if (aiDK.mana <= 2 || aiMega.mana <= 2 || aiLK.mana <= 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackPhaseThree(e);
                        }
                        
                    }
                    break;
                case 2:
                    if (mega.health >= 13 && mega.health <= 20 )
                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackFirstPhase(e);
                        }

                    }
                    else if (mega.health <= 12 && mega.health > 10)
                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackSecondPhase(e);
                        }
                    }
                    else if (mega.health <= 10)
                    {
                        if (aiDK.mana <= 2 || aiMega.mana <= 2 || aiLK.mana <= 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackPhaseThree(e);
                        }
                    }
                    break;
                case 3:
                    if (lk.health >= 13 && lk.health <= 20)
                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackFirstPhase(e);
                        }
                    }
                    else if (lk.health <= 12 && lk.health > 10 )
                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        {
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackSecondPhase(e);
                        }
                    }
                    else if (lk.health <= 10 )
                    {
                        if (aiDK.mana < 2 || aiMega.mana < 2 || aiLK.mana < 2)
                        { 
                            notEnoughMana(e);
                        }
                        else
                        {
                            aiAttackPhaseThree(e);
                        }
                    }
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {

            //Collsion Logic
            switch (OrignalForm.player_Charcter)
            {
                case 1:
                    {
                        if ((dk.PlayerCollsion(aiMega, aiDK, aiLK) == true))
                        {
                            CollsionDetail();
                        }
                        break;
                    }
                case 2:
                    {
                        if (mega.PlayerCollsion(aiMega, aiDK, aiLK) == true)
                        {
                            CollsionDetail();
                        }
                    }
                    break;
                case 3:
                    {
                        if (lk.PlayerCollsion(aiMega, aiDK, aiLK) == true)
                        {
                            CollsionDetail();
                        }
                    }
                    break;
            }
            switch (aiSelectedCharcter)
            {
                case 1:
                    {
                        if ((aiDK.Collsion(mega, dk, lk) == true))
                        {
                            CollsionDetail();
                        }
                        break;
                    }
                case 2:
                    {
                        if ((aiMega.Collsion(mega, dk, lk) == true))
                        {
                            CollsionDetail();
                        }
                    }
                    break;
                case 3:
                    {
                        if (aiLK.Collsion(mega, dk, lk) == true)
                        {
                            CollsionDetail();
                        }
                    }
                    break;
            }
            //Used in Attacks to keep them going
            if (possibleAttacks == true && keyPress == true || timer > 20 && insufficientMana == false)
            {
                timer++;
            }
            //Used to fill mana under specfic cirumstances
            if (passPhase == true)
            {
                switch (OrignalForm.player_Charcter)
                {
                    case 1:
                        dk.mana += 3;
                        passPhase = false;
                        if (dk.mana > 10)
                        {
                            dk.mana = 10;
                        }
                        if (aiTurn == false)
                        {
                            aiTurn = true;
                        }
                        break;
                    case 2:
                        mega.mana += 3;
                        passPhase = false;
                        if (mega.mana > 10)
                        {
                            mega.mana = 10;
                        }
                        if (aiTurn == false)
                        {
                            aiTurn = true;
                        }
                        break;
                    case 3:
                        lk.mana += 3;
                        passPhase = false;
                        if (lk.mana > 10)
                        {
                            lk.mana = 10;
                        }
                        if (aiTurn == false)
                        {
                            aiTurn = true;
                        }
                        break;
                }
            }


            Refresh();

        }
        //Ensures total rest when you can't atack
        private void notEnoughMana(PaintEventArgs e)
        {
            if (aiTurn == false)
            {
                insufficientMana = true;
                possibleAttacks = false;
                attackPhase = false;
                manaNoCheat = false;
                attackSelector = 0;
                startingOptions(e);
            }
            //Gives AI mana when they can't attack,also prevents going over limit of 10
            else
            {
                switch (aiSelectedCharcter)
                {
                    case 1:
                        aiDK.mana += 3;
                        if (aiDK.mana > 10)
                        {
                            aiDK.mana = 10;
                        }
                        break;
                    case 2:
                        aiMega.mana += 3;
                        if (aiMega.mana > 10)
                        {
                            aiMega.mana = 10;
                        }
                        break;
                    case 3:
                        aiLK.mana += 3;
                        if (aiLK.mana > 10)
                        {
                            aiLK.mana = 10;
                        }
                        break;
                }
                e.Graphics.DrawString("Other Player does not have enough Mana!", drawFont, drawBrush, 200, 0);
                aiTurn = false;
                randomlyGeneratedAttack = false;
            }

        }
        //Used in Classes for efficencey
        public static void AttackEndDetails()
        {
            drawBrush.Color = Color.DarkSlateBlue;
            insufficientMana = false;
            possibleAttacks = false;
            if (aiTurn == false) { attackInMotion = true; }
            else { attackInMotion = false; }
            manaNoCheat = false;

        }
        //Draws what attack the player selects for the apporiate charcter and attack and also what happens when they can't
        private void attackDrawer(PaintEventArgs e)
        {


            if (attackSelector == 1 && possibleAttacks == true || aiAttackIndicator == 1 && aiTurn == true || timer > 20 && attackInMotion == true && attackSelector == 1)
            {
                switch (OrignalForm.player_Charcter)
                {

                    case 1:
                        if (dk.mana >= 2)
                        {
                            dk.DeathEyes(e, dk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 2:
                        if (mega.mana >= 2)
                        {
                            mega.explosion(e, mega);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 3:
                        if (lk.mana >= 2)
                        {
                            lk.Sparks(e, lk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                }

            }
            if (attackSelector == 2 && possibleAttacks == true || timer > 20 && attackInMotion == true && attackSelector == 2)
            {
                switch (OrignalForm.player_Charcter)
                {
                    case 1:
                        if (dk.mana >= 5)
                        {
                            dk.DeathHand(e, dk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 2:
                        if (mega.mana >= 5)
                        {
                            mega.Explosion(e, mega);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 3:
                        if (lk.mana >= 5)
                        {
                            lk.Crackle(e, lk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                }
            }
            if (attackSelector == 3 && possibleAttacks == true || timer > 20 && attackInMotion == true && attackSelector == 3)
            {
                switch (OrignalForm.player_Charcter)
                {

                    case 1:
                        if (dk.mana >= 10)
                        {
                            dk.DeathSword(e, dk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 2:
                        if (mega.mana >= 10)
                        {
                            mega.EXPLOSION(e, mega);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 3:
                        if (lk.mana >= 10)
                        {
                            lk.Lighting(e, lk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                }
            }
            if (attackSelector == 4 && possibleAttacks == true || timer > 20 && attackInMotion == true && attackSelector == 4)
            {
                switch (OrignalForm.player_Charcter)
                {
                    case 1:
                        if (dk.mana >= 3)
                        {
                            dk.Shout(e, dk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }

                        break;
                    case 2:

                        if (mega.mana >= 3)
                        {
                            mega.Baka(e, mega);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                    case 3:
                        if (lk.mana >= 3)
                        {
                            lk.Lizards(e, lk);
                        }
                        else
                        {
                            notEnoughMana(e);
                        }
                        break;
                }
            }

        }

        //Drawn when Staring up the game or when going back to the start
        public void startingOptions(PaintEventArgs e)
        {
            if (insufficientMana == true && aiTurn == false)
            {
                e.Graphics.DrawString("Not Enough Mana", drawFont, drawBrush, 200, 0);
            }
            if (attackSelector == 0 && insufficientMana == false && aiTurn == false)
            {
                drawBrush.Color = Color.Black;
                e.Graphics.DrawString("What Will You Do?", drawFont, drawBrush, 200, 0);
            }
            if (attackPhase == false && aiTurn == false)
            {
                drawBrush.Color = Color.Green;
                e.Graphics.DrawString("Attack", drawFont, drawBrush, 100, 330);
                drawBrush.Color = Color.SkyBlue;
                e.Graphics.DrawString("Defend", drawFont, drawBrush, 500, 330);
                drawBrush.Color = Color.Red;
                e.Graphics.DrawString("Pass", drawFont, drawBrush, 90, 410);
                drawBrush.Color = Color.Yellow;
                e.Graphics.DrawString("Useless", drawFont, drawBrush, 500, 410);
            }
        }



        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            if(fun == true)
            {
                HavingFun(e);
            }
            
            //Drawn here
            startingOptions(e);

            //Drawing Ai Charcter
            switch (aiSelectedCharcter)
            {
                case 1:
                    e.Graphics.DrawImage(Properties.Resources.DeathWizard, aiDK.aiX, aiDK.aiY);
                    e.Graphics.DrawString(Convert.ToString(aiDK.mana) + "/10", drawFont, manaBrush, 700, 52);
                    e.Graphics.DrawString(Convert.ToString(aiDK.health) + "/20", drawFont, healthBrush, 700, 20);
                    break;
                case 2:
                    e.Graphics.DrawImage(Properties.Resources.MEGAMEME, aiMega.aiX, aiMega.aiY);
                    e.Graphics.DrawString(Convert.ToString(aiMega.mana) + "/10", drawFont, manaBrush, 700, 52);
                    e.Graphics.DrawString(Convert.ToString(aiMega.health) + "/20", drawFont, healthBrush, 700, 20);
                    break;
                case 3:
                    e.Graphics.DrawImage(Properties.Resources.Light_Wizard, aiLK.aiX, aiLK.y);
                    e.Graphics.DrawString(Convert.ToString(aiLK.mana) + "/10", drawFont, manaBrush, 700, 52);
                    e.Graphics.DrawString(Convert.ToString(aiLK.health) + "/20", drawFont, healthBrush, 700, 20);
                    break;
            }
            //Drawing Player Charcter
            switch (OrignalForm.player_Charcter)
            {
                case 1:
                    e.Graphics.DrawImage(Properties.Resources.DeathWizard, dk.x, dk.y);
                    e.Graphics.DrawString(Convert.ToString(dk.mana) + "/10", drawFont, manaBrush, 7, 52);
                    e.Graphics.DrawString(Convert.ToString(dk.health) + "/20", drawFont, healthBrush, 10, 20);
                    break;
                case 2:
                    e.Graphics.DrawImage(Properties.Resources.MEGAMEME, mega.x, mega.y);
                    e.Graphics.DrawString(Convert.ToString(mega.mana) + "/10", drawFont, manaBrush, 7, 52);
                    e.Graphics.DrawString(Convert.ToString(mega.health) + "/20", drawFont, healthBrush, 10, 20);
                    break;
                case 3:
                    e.Graphics.DrawImage(Properties.Resources.Light_Wizard, lk.x, lk.y);
                    e.Graphics.DrawString(Convert.ToString(lk.mana) + "/10", drawFont, manaBrush, 7, 52);
                    e.Graphics.DrawString(Convert.ToString(lk.health) + "/20", drawFont, healthBrush, 10, 20);
                    break;
            }
            //Switches around what is drawn when attacking and based off of what wizard you selected
            if (attackPhase == true)
            {
                switch (OrignalForm.player_Charcter)
                {
                    case 1:
                        drawBrush.Color = Color.Green;
                        e.Graphics.DrawString("Death Eyes", drawFont, drawBrush, 100, 330);
                        drawBrush.Color = Color.SkyBlue;
                        e.Graphics.DrawString("Death Hand", drawFont, drawBrush, 500, 330);
                        drawBrush.Color = Color.Red;
                        e.Graphics.DrawString("Death Sword", drawFont, drawBrush, 90, 410);
                        drawBrush.Color = Color.Yellow;
                        e.Graphics.DrawString("Shout", drawFont, drawBrush, 500, 410);
                        drawBrush.Color = Color.Green;
                        possibleAttacks = true;
                        break;
                    case 2:
                        drawBrush.Color = Color.Green;
                        e.Graphics.DrawString("expolsion", drawFont, drawBrush, 100, 330);
                        drawBrush.Color = Color.SkyBlue;
                        e.Graphics.DrawString("Explosion", drawFont, drawBrush, 500, 330);
                        drawBrush.Color = Color.Red;
                        e.Graphics.DrawString("EXPLOSION", drawFont, drawBrush, 90, 410);
                        drawBrush.Color = Color.Yellow;
                        e.Graphics.DrawString("Baka", drawFont, drawBrush, 500, 410);
                        possibleAttacks = true;
                        break;
                    case 3:
                        drawBrush.Color = Color.Green;
                        e.Graphics.DrawString("Sparks", drawFont, drawBrush, 100, 330);
                        drawBrush.Color = Color.SkyBlue;
                        e.Graphics.DrawString("Crakle", drawFont, drawBrush, 500, 330);
                        drawBrush.Color = Color.Red;
                        e.Graphics.DrawString("Lightining", drawFont, drawBrush, 90, 410);
                        drawBrush.Color = Color.Yellow;
                        e.Graphics.DrawString("Lizards", drawFont, drawBrush, 500, 410);
                        possibleAttacks = true;
                        break;
                }
            }
            //Drawing the possible attacks at the start and while in motion
            if (possibleAttacks == true || attackInMotion == true)
            {
                attackDrawer(e);
            }
            //Draws Ai's attacks
            if (aiTurn == true)
            {
                aiPhase(e);
            }
        }


    }
}

