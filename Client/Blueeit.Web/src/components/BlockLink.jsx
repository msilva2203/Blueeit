import "./BlockLink.css";

export default function BlockLink({content, ref, isSelected}) {
    return (
        <>
            <a className={`blueeit-block-link ${isSelected ? ("is-selected") : ("")}`} href={ref}>
                {content}
            </a>
        </>
    );
}