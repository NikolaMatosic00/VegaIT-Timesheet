import React from 'react'
import AlphabetComponent from '../../../common/components/AlphabetComponent'
import PaginationComponent from '../../../common/components/PaginationComponent'

function ProjectsPage() {
	return (
		<div className="mcn">
			<div className="main-content">
				<h2 className="main-content__title">Projects</h2>
				<div className="table-navigation">
					<a href="javascript:;" className="table-navigation__create btn-modal"><span>Create new project</span></a>
					<form className="table-navigation__input-container" action="javascript:;">
						<input type="text" className="table-navigation__search" />
						<button type="submit" className="icon__search"></button>
					</form>
				</div>
				<AlphabetComponent />
				<PaginationComponent />
			</div>
		</div>
	)
}

export default ProjectsPage