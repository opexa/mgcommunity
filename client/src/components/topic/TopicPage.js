import React from 'react'
import topicActions from '../../actions/TopicActions'
import topicStore from '../../stores/TopicStore'

class TopicPage extends React.Component {
  constructor(props) {
    super(props)

    this.state = {
      id: this.props.match.params.id,
      info: {},
      page: 1,
      replies: []
    }

    this.handleInfoFetched = this.handleInfoFetched.bind(this)
    this.handleRepliesFetched = this.handleRepliesFetched.bind(this)

    topicStore.on(topicStore.eventTypes.INFO_FETCHED, this.handleInfoFetched)
    topicStore.on(topicStore.eventTypes.REPLIES_FETCHED, this.handleRepliesFetched)
  }
  
  componentWillMount () {
    topicActions.getInfo(this.state.id)
    // topicActions.getReplies(this.state.id, this.state.page)
  }

  handleInfoFetched (info) {
    this.setState({ info })
  }

  handleRepliesFetched(replies) {
    this.setState(prevState => ({ 
      replies: replies,
      page: prevState.page += 1
     }))
  }

  render () {
    return (
      <div>Hahah</div>
    )
  }
}

export default TopicPage
