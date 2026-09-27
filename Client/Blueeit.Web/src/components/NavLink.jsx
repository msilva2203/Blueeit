import './NavLink.css'

export default function NavLink({ href, children }) {
    return (
        <>
            <a className="blueeit-navlink-item" href={href}>
                {children}
            </a>
        </>
    );
}