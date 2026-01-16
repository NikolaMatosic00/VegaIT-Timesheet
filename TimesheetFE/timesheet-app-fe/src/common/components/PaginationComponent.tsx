import React from 'react'

function PaginationComponent() {
  return (
    <div className="pagination">
						<ul className="pagination__navigation">
							<li className="pagination__list">
								<a className="pagination__button" href="javascript:;">Previous</a>
							</li>
							<li className="pagination__list">
								<a className="pagination__button pagination__button--active" href="javascript:;">1</a>
							</li>
							<li className="pagination__list">
								<a className="pagination__button" href="javascript:;">2</a>
							</li>
							<li className="pagination__list">
								<a className="pagination__button" href="javascript:;">3</a>
							</li>
							<li className="pagination__list">
								<a className="pagination__button" href="javascript:;">4</a>
							</li>
							<li className="pagination__list">
								<a className="pagination__button" href="javascript:;">Next</a>
							</li>
						</ul>
					</div>
  )
}

export default PaginationComponent