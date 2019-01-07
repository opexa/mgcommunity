import React from 'react'
import { Link } from 'react-router-dom'

const Topic = (props) => (
  <div className='topic'>
    <div className='topic-heading'>
      <div className='topic-creator-avatar'>
        <Link to={`/user/${props.topic.authorUsername}`}>
          <img src={props.topic.authorAvatar} alt=''/>
        </Link>
      </div>
      <div>
        <p className='topic-title'>
          <Link to={`/category/${props.categoryId}/topic/${props.topic.id}`}>{props.topic.title}</Link>
        </p>
        <div className='topic-creation'>
          от <Link to={`/user/${props.topic.authorUsername}`}>
                <b>{props.topic.authorUsername} </b>
              </Link>
          - {props.topic.createdOn}
        </div>
      </div>
    </div>
    <div className='topic-replies-info'>
      <div className='topic-last-reply'>
        <div>
          Последно&nbsp;
          <Link to={`/user/${props.topic.lastReplyBy}`}>
            <b>{props.topic.lastReplyBy} </b>
          </Link>
        </div>
        <div>{props.topic.lastReply}</div>
      </div>
      <div className='participants-info'>
        <div className='topic-participants-count'>
          <span>Участници: </span>
          <span>{props.topic.participantsCount}</span>
        </div>
        <div className='topic-replies'>
          <span>Коментари: </span>
          <span>{props.topic.repliesCount}</span>
        </div>
      </div>
    </div>
  </div>
)

export default Topic