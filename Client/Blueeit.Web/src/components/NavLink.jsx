import './NavLink.css'

export default function NavLink({ href, children, isSelected }) {
    return (
        <>
            <a className={`blueeit-navlink-item ${isSelected ? ("is-selected") : ("")}`} href={href}>
                {children}
            </a>
        </>
    );
}