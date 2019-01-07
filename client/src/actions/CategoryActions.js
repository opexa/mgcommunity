import dispatcher from '../dispatcher'

const CategoryActions = {
  types: {
    GET_FEED: 'GET_FEED',
    GET_NAME: 'GET_NAME'
  },
  getFeed (params) {
    dispatcher.dispatch({
      type: this.types.GET_FEED,
      params
    })
  },
  getName (id) {
    dispatcher.dispatch({
      type: this.types.GET_NAME,
      id
    })
  }
}

export default CategoryActions
