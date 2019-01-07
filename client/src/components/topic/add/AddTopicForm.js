import React from 'react'

const AddTopicForm = (props) => (
  <form className='add-topic-form'>
    <div className="topic-title">
      <input type="text" name='title' value={props.topic.title} onChange={props.onChange} autocomplete='off' placeholder='Заглавие' />
    </div>
    <div className="first-reply">
      <div className="first-reply-toolbar">
        TOOLBAR
      </div>
      <textarea name="content" id="content" cols="30" rows="10" value={props.topic.content} onChange={props.onChange}></textarea>
    </div>
  </form>
)

export default AddTopicForm