import React from 'react'

const Input = ({ type, name, label, placeholder, labelClass, value, onChange, ...rest}) => {
  let _type = type || 'text'
  let _label = label || placeholder

  return (
    <div className='form-group'  {...rest} >
      <label className={labelClass} htmlFor={name}>{_label}</label>
      <input
        className='form-control'
        type={_type}
        name={name}
        placeholder={placeholder}
        value={value}
        onChange={onChange} />
    </div>
  )
}

export default Input
