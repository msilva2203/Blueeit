import "./BodyHeader.css";

export default function BodyHeader({title}) {
    return (
        <>
            <div className="blueeit-bodyheader">
                <h1>
                    {title}
                </h1>
            </div>
        </>
    );
}