import dispatcher from '../dispatcher'

const TopicActions = {
  types: {
    GET_REPLIES: 'GET_REPLIES',
    GET_INFO: 'GET_INFO',
    ADD_TOPIC: 'ADD_TOPIC'
  },
  getReplies(params) {
    dispatcher.dispatch({
      type: this.types.GET_REPLIES,
      params
    })
  },
  getInfo(id) {
    dispatcher.dispatch({
      type: this.types.GET_INFO,
      id
    })
  },
  addTopic(topic) {
    dispatcher.dispatch({
      type: this.types.ADD_TOPIC,
      topic
    })
  }
}

export default TopicActions