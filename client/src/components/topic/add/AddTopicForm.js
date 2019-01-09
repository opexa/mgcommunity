import React from 'react'

const AddTopicForm = (props) => (
  <form className='add-topic-form'>
    <div className="topic-title">
      <input type="text" name='title' value={props.topic.title} onChange={props.onChange} autoComplete='off' placeholder='Заглавие' />
    </div>
    <div className="first-reply">
      <div className="first-reply-toolbar">
        TOOLBAR
      </div>
      <textarea name="content" id="content" value={props.topic.content} onChange={props.onChange}></textarea>
    </div>
    <div className="new-topic-actions">
      <input type="submit" className="btn btn-primary btn-lg new-topic-submit" value='Добави' onClick={props.onSubmit} />
    </div>
  </form>
)

export default AddTopicForm