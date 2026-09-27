import { Link } from "react-router-dom";

import "./Breadcrumbs.css";

export default function Breadcrumbs({items}) {
    const isVisible = items.length > 0;

    if (!isVisible) {
        return (
            <>
            </>
        );
    }

    return (
        <>
            <nav aria-label="Breadcrumb" className="blueeit-breadcrumbs-main">
                { items.map((item) => (
                    <div className="blueeit-breadcrumbs-item">
                        <Link to={`${item.url}`}>
                            <h3>
                                {item.name}
                            </h3>
                        </Link>
                        <span>›</span>
                    </div>
                ))}
            </nav>
        </>
    );
}