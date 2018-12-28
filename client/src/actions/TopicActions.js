import dispatcher from '../dispatcher'

const TopicActions = {
  types: {
    GET_REPLIES: 'GET_REPLIES',
    GET_INFO: 'GET_INFO'
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
  }
}

export default TopicActions