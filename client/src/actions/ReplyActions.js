import dispatcher from '../dispatcher'

const ReplyActions = {
  types: {
    ADD_REPLY: 'ADD_REPLY'
  },
  addReply(data) {
    dispatcher.dispatch({
      type: this.types.ADD_REPLY,
      data
    })
  }
}

export default ReplyActions