import dispatcher from '../dispatcher'

const ReplyActions = {
  type: {
    ADD_REPLY: 'ADD_REPLY'
  },
  addReply(content) {
    dispatcher.dispatch({
      type: this.types.ADD_REPLY,
      content
    })
  }
}

export default ReplyActions