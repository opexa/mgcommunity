import dispatcher from '../dispatcher'

const HomeActions = {
  types: {
    GET_SECTIONS: 'GET_SECTIONS'
  },
  getSections() {
    dispatcher.dispatch({
      type: this.types.GET_SECTIONS
    })
  }
}

export default HomeActions
