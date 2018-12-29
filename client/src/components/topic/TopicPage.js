import React from 'react'
import { Link } from 'react-router-dom'
import topicActions from '../../actions/TopicActions'
import topicStore from '../../stores/TopicStore'
import ReplyElement from './ReplyElement'

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
    let state = this.state
    topicActions.getInfo(state.id)
    topicActions.getReplies({
      id: state.id, 
      page: state.page
    })
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
    let info = this.state.info
    let replies = this.state.replies

    return (
      <div>
        <div className="topic-info">
          <div className="title">{info.title}</div>
          <div className="creation">
            от <Link className="topic-author" to={`/user/${info.authorUsername}`}><b>{info.authorUsername}</b></Link>
            <span className="topic-created-on">  {info.createdOn}</span>
          </div>
        </div>
        <div className="topic-replies">
          {replies.map(reply => (
            <ReplyElement key={reply.id} reply={reply} />
          ))}
        </div>
      </div>
    )
  }
}

export default TopicPage
