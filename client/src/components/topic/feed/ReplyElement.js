import React from 'react'
import { Link } from 'react-router-dom'

class ReplyElement extends React.Component {
  constructor(props) {
    super(props)

    this.state = {
      reply: props.reply
    }
  }
  render () {
    let reply = this.state.reply

    return (
      <div className='reply'>
        <div className="author-panel">
          <div className="author-avatar">
            <img src={reply.authorAvatar} alt='Профилна снимка на автора'/>
          </div>
          <div className="author-username">
            <Link to={`/user/${reply.authorUsername}`}>{reply.authorUsername}</Link>
          </div>
          <div className="author-status">
            {reply.authorStatus}
          </div>
        </div>
        <div className="reply-content-wrapper">
          <div className="reply-creation">
            <div className="reply-relative-date">{reply.relativeDate}</div> 
            <div className="reply-detailed-date">{reply.detailedDate}</div>
          </div>
          <div className="reply-content">{reply.content}</div>
          <div className="reply-actions">
            <Link to={`/reply/report?id=${reply.id}`}>Report</Link>
          </div>
        </div>
      </div>
    )
  }
}

export default ReplyElement