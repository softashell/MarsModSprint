using BepInEx;
using RoR2;

namespace MarsModSprint
{
    [BepInPlugin("com.marsupilami.marsmodsprint", "MarsModSprint", "1.0.3")]
    public class MarsModSprint : BaseUnityPlugin
    {
        private bool mmIsSprinting;

        public void Awake()
        {
            mmIsSprinting = false;
        }

        public void Update()
        {
            if (!Run.instance)
                return;

            var player = NetworkUser.readOnlyLocalPlayersList[0];

            if (player.inputPlayer.GetButtonUp("Sprint"))
            {
                mmIsSprinting = !mmIsSprinting;
            }
        }

        public void FixedUpdate()
        {
            if (!Run.instance)
                return;

            var player = NetworkUser.readOnlyLocalPlayersList[0];
            var bodyInputs = player?.master?.GetBodyObject()?.GetComponent<InputBankTest>();
            if (!bodyInputs)
                return;

            var isAttacking = bodyInputs.skill1.down || bodyInputs.skill2.down || bodyInputs.skill3.down || bodyInputs.skill4.down;

            if (mmIsSprinting && !isAttacking)
            {
                bodyInputs.sprint.PushState(!player.inputPlayer.GetButton("Sprint"));
            }
        }
    }
}