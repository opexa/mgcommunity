import React from 'react'
import { Link } from 'react-router-dom'

const Section = (props) => (
  <div className='section'>
    <div className='section-name'>{props.section.name.toUpperCase()}</div> 
    <div className='categories-container'>
      {props.section.categories.map(category => (
        <div className='category' key={category.id}>
          <div className='category-info'>
            <p className='category-name'><Link to={`/category/${category.id}`}>{category.name}</Link></p>
            <p className="category-description">{category.description}</p>
          </div>
          <div className='category-actions'>
            <span className='topics-count'>{category.topicsCount} теми</span>
            {/* LAST REPLY */}
            {/* REPLIES COUNT */}
          </div>
        </div>
      ))}
    </div>
  </div>
)

export default Section