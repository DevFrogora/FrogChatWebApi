using FrogChatModel.DTOModel;

namespace FrogChatWebApi.Hubs
{
    public class ChatPersistenceData
    {
        private int messageId;
        public int MessageId
        {
            get
            {
                messageId++;
                return messageId;
            }
        }

        public List<UserDto> users;

    }
}
