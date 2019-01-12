import React from 'react'

const AccountDetails = (props) => (
  <div className="account-details-content">
    <div className="account-profile-picture">
      <img src={props.user.avatar} alt=""/>
    </div>
    <div className="account-profile-username">
      <p>{props.user.userName}</p>
    </div>
    <div className="account-profile-names">
      <p className="account-names-heading">Имена</p>
      <p className="account-names-contetn">{props.user.firstName} {props.user.lastName}</p>
    </div>
  </div>
)

export default AccountDetails