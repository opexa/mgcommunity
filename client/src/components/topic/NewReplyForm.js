import React from 'react'
import Auth from '../account/Auth'

const NewReplyForm = (props) => (
  <div className="new-reply">
    <form className='new-reply-form'>
      <div className="reply-author-picture">
        <div className="picture-container">
          <img src={Auth.getUser().avatar} alt=""/>
        </div>
      </div>
      <div className="reply-content">
        <div className="toolbar">
          TOOLBAR 
        </div>
        <div className="content-input-container">
          <textarea className='content-input' id='replyContent' name='content' placeholder='Напиши своя коментар ...' onChange={props.onChange} value={props.content}></textarea>
        </div>
        <div className='post-reply'>
          <input type='submit' className='post-reply-button btn btn-primary' value='Коментирай' onClick={props.onSubmit}/>
        </div>
      </div>
    </form>
  </div>
)

export default NewReplyForm
