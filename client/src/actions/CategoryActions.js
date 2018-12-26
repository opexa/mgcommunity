import dispatcher from '../dispatcher'

const CategoryActions = {
  types: {
    GET_FEED: 'GET_FEED'
  },
  getFeed (params) {
    dispatcher.dispatch({
      type: this.types.GET_FEED,
      params
    })
  }
}

export default CategoryActions
