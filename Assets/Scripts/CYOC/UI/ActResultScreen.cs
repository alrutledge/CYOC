using UnityEngine;
using Assets.Scripts.ICG.Messaging;
using Assets.Scripts.CYOC.UI.Messages;
using Assets.Scripts.ChoiceEngine.Messages;
using UnityEngine.UI;
using Assets.Scripts.ChoiceEngine;


namespace Assets.Scripts.CYOC.UI
{
    public class ActResultScreen : MonoBehaviour
    {
        public Text Secret1Text;
        public Text Secret1Percent;
        public Text Secret2Text;
        public Text Secret2Percent;
        public Text Secret3Text;
        public Text Secret3Percent;
        public Text Secret4Text;
        public Text Secret4Percent;
        public Text Secret5Text;
        public Text Secret5Percent;
        public Text Secret6Text;
        public Text Secret6Percent;
        public Text TotalPercent;
        public Text ActName;

        private void Awake()
        {
            MessageSystem.SubscribeMessage<ShowActEndCommand>(MessageSystem.ServiceContext, OnShowActEndCommand);
        }

        private void OnDestroy()
        {
            MessageSystem.UnsubscribeMessage<ShowActEndCommand>(MessageSystem.ServiceContext, OnShowActEndCommand);
        }

        private void OnShowActEndCommand(ShowActEndCommand message)
        {
            int percentage = 0;

            // gather each of the requirements and set the text and percentage complete for each.
            GetCurrentActReply actReply = MessageSystem.BroadcastQuery<GetCurrentActReply, GetCurrentActQuery>(new GetCurrentActQuery());
            ActName.text = actReply.CurrentAct.Name;

            if (ActName.text == "Act 1 - Morning")
            {
                ChoiceRequirement dreamCthulhuRequirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Dreamt of Cthulhu");
                ChoiceRequirement dreamRlyehRequirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Dreamt of Rlyeh");
                ChoiceRequirement heardCarolynRequirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Heard Carolines Recording");
                ChoiceRequirement sawMirrorVisionRequirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Saw the Mirrors Vision");
                ChoiceRequirement enteredTheVaultRequirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Entered the Vault");
                ChoiceRequirement completedAct1Requirement = new ChoiceRequirement(ChoiceRequirementType.HAVE_FLAG, "Completed Act 1");

                RequirementReply reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(dreamCthulhuRequirement));
                if (reply.RequirementMet)
                {
                    Secret1Text.text = "Dreamt of Cthulhu";
                    Secret1Percent.text = "15";
                    percentage += 15;
                }
                else
                {
                    Secret1Text.text = "Secret 1";
                    Secret1Percent.text = "??";
                }
                reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(dreamRlyehRequirement));
                if (reply.RequirementMet)
                {
                    Secret2Text.text = "Dreamt of Rl'yeh";
                    Secret2Percent.text = "15";
                    percentage += 15;
                }
                else
                {
                    Secret2Text.text = "Secret 2";
                    Secret2Percent.text = "??";
                }
                reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(heardCarolynRequirement));
                if (reply.RequirementMet)
                {
                    Secret3Text.text = "Carolyn's Recording";
                    Secret3Percent.text = "15";
                    percentage += 15;
                }
                else
                {
                    Secret3Text.text = "Secret 3";
                    Secret3Percent.text = "??";
                }
                reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(sawMirrorVisionRequirement));
                if (reply.RequirementMet)
                {
                    Secret4Text.text = "Mirror's Vision";
                    Secret4Percent.text = "15";
                    percentage += 15;
                }
                else
                {
                    Secret4Text.text = "Secret 4";
                    Secret4Percent.text = "??";
                }
                reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(enteredTheVaultRequirement));
                if (reply.RequirementMet)
                {
                    Secret5Text.text = "Entered the Vault";
                    Secret5Percent.text = "20";
                    percentage += 20;
                }
                else
                {
                    Secret5Text.text = "Secret 5";
                    Secret5Percent.text = "??";
                }
                reply = MessageSystem.BroadcastQuery<RequirementReply, RequirementQuery>(new RequirementQuery(completedAct1Requirement));
                if (reply.RequirementMet)
                {
                    Secret6Text.text = "Meet the Cult";
                    Secret6Percent.text = "20";
                    percentage += 20;
                }
                else
                {
                    Secret6Text.text = "Secret 6";
                    Secret6Percent.text = "??";
                }
            }
            else if (ActName.text == "Act 2 - Afternoon")
            {
                // setup Act 2 secrets
            }
            else 
            {
                // setup Act 3 secrets
            }

            // set the total percentage complete
            TotalPercent.text = percentage.ToString();
        }

        public void OnContinueClicked()
        {
            MessageSystem.BroadcastMessage(new ExitToMainMenuCommand());
        }
    }
}