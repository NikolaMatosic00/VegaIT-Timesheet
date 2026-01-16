import React from 'react'
import AlphabetComponent from '../../../common/components/AlphabetComponent'
import PaginationComponent from '../../../common/components/PaginationComponent'


//ClientsSpacesComponent treba da povuce instant koliko ClientSpace komponenti da napravi, saljem prvi zahtev pri kliku na Clients u navbaru i default je prikaz 3 po strani prva strana
function CategoriesPage() {
    return (
        <div className="mcn">
            <div className="main-content">
                <h2 className="main-content__title">Category</h2>
                <div className="table-navigation">
                    <a href="javascript:;" className="table-navigation__create btn-modal"><span>Create new category</span></a>
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

export default CategoriesPage